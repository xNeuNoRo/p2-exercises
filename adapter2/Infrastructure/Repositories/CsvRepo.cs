using System.Collections.Concurrent;
using System.Globalization;
using System.Reflection;

namespace App.Infrastructure.Repositories;

public class CsvRepo<T>
    where T : class, new() // Restringimos T para que sea una clase que tenga un constructor sin parametros
// Ya que de esa forma podemos crear instancias de T dentro de la clase y asignar manualmente sus propiedades 1 por 1
{
    // Campos requeridos para el manejo de archivos CSV
    private readonly string _filePath; // Ruta del archivo CSV
    private readonly PropertyInfo[] _properties; // Propiedades de la clase T
    private const string Separator = ","; // Separador entre valores en el CSV

    // SINGLETON POR ARCHIVO (O TABLA)
    // Diccionario concurrente para almacenar las instancias de CsvRepo<T> por ruta de archivo, garantizando que solo haya una instancia por archivo (o tabla basicamente ya que simula una tabla sql)
    // El diccionario concurrente es igual que un diccionario normal pero con la seguridad de que es atomico
    private static readonly ConcurrentDictionary<string, CsvRepo<T>> _instances = new();

    // Constructor que inicializa la ruta del archivo y las propiedades de T
    private CsvRepo(string filePath)
    {
        _filePath = filePath; // Inicializa la ruta del archivo
        // Con typeof(T) obtenemos todos los detalles de la clase T (basicamente como moldeamos la clase y mas datos)
        // Y GetProperties() obtiene todas sus propiedades
        _properties = typeof(T).GetProperties();
        EnsureHeader(); // Asegura que el archivo tenga un header con los nombres de las propiedades de T
    }

    // Metodo para obtener la instancia unica de CsvRepo<T> para una ruta de archivo dada
    public static CsvRepo<T> GetInstance(string filePath) =>
        _instances.GetOrAdd(filePath, fp => new CsvRepo<T>(fp));

    // Metodo para escribir los headers del CSV con los nombres de las propiedades de T
    private void WriteHeader()
    {
        // Crea una linea con los nombres de las propiedades separadas por el separador definido
        var header = string.Join(Separator, _properties.Select(p => p.Name));
        // Escribe la linea de header en el archivo (o sobreescribe si ya existe)
        File.WriteAllText(_filePath, header + Environment.NewLine);
        // EJ: Si T es Persona con propiedades Nombre y Edad,
        // el archivo CSV tendra la primera linea: "Nombre,Edad"
    }

    // Metodo para asegurar que el archivo tenga un header, lo llama antes de agregar o guardar items
    private void EnsureHeader()
    {
        // Si el archivo no existe o esta vacio, escribe el header
        if (!File.Exists(_filePath) || new FileInfo(_filePath).Length == 0)
            WriteHeader();
    }

    // Metodo para agregar un nuevo item al archivo
    public void AppendItem(T item)
    {
        EnsureHeader(); // Asegura que el header este presente antes de agregar una linea
        File.AppendAllText(_filePath, ToLine(item) + "\n");
    }

    // Metodo para guardar todos los items en el archivo (sobrescribe lo que haya)
    public void SaveCsv(List<T> items)
    {
        // Primero escribe el header
        WriteHeader();
        // Luego agrega todas las lineas correspondientes a los items
        File.AppendAllLines(_filePath, items.Select(ToLine));
    }

    // Metodo para cargar todos los items del archivo
    public List<T> GetCsv()
    {
        // Si el archivo no existe, retorna una lista vacia
        if (!File.Exists(_filePath))
            return new List<T>();

        // Lee todas las lineas del archivo
        var allLines = File.ReadAllLines(_filePath);

        // Si no hay nada o solo header
        if (allLines.Length <= 1)
            return new List<T>();

        // Lee el header para construir el mapa de columnas a propiedades
        var headerCols = allLines[0].Split(Separator);
        var map = BuildColumnMap(headerCols);

        // Por cada linea (omitiendo el header), la parsea a un objeto T usando el metodo ParseLine con el mapa construido, y retorna la lista de objetos
        return allLines
            .Skip(1)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => ParseLine(line, map))
            .ToList();
    }

    // Convierte un objeto T a una linea de texto en formato CSV
    private string ToLine(T item) =>
        string.Join(Separator, _properties.Select(p => p.GetValue(item)));

    // Construye un mapa de indice de columna a propiedad de T, basado en el header del archivo CSV
    // De esta forma, aunque el orden de las columnas en el CSV cambie, el parseo seguira funcionando correctamente
    // EJ: Si el header del CSV es "Edad,Nombre" y T tiene propiedades Nombre y Edad, el mapa sera:
    // 0 => Propiedad Edad
    // 1 => Propiedad Nombre
    private Dictionary<int, PropertyInfo> BuildColumnMap(string[] headerCols)
    {
        // Creamos un diccionario para mapear el indice de cada columna en el CSV con la propiedad correspondiente de T
        var map = new Dictionary<int, PropertyInfo>();

        // Por cada columna en el header
        for (int i = 0; i < headerCols.Length; i++)
        {
            // Buscamos la propiedad de T que tenga el mismo nombre que la columna (ignorando espacios)
            var colName = headerCols[i].Trim();
            var prop = _properties.FirstOrDefault(p =>
                string.Equals(p.Name, colName, StringComparison.OrdinalIgnoreCase) // Compara el nombre de la propiedad con el nombre de la columna, ignorando mayusculas y minusculas
            );
            // Si encontramos una propiedad que coincide con el nombre de la columna, la agregamos al mapa con su indice
            if (prop != null)
                map[i] = prop;
        }

        // Retornamos el mapa construido, que nos permitira luego asignar los valores de cada columna a la propiedad correcta de T al parsear las lineas del CSV
        return map;
    }

    private static T ParseLine(string line, Dictionary<int, PropertyInfo> map)
    {
        // Crea una nueva instancia de T vacia
        var item = new T();

        // Divide la linea por el separador definido
        var values = line.Split(Separator);

        // Por cada par indice-columna en el mapa,
        // asignamos el valor correspondiente de la linea a la propiedad de T indicada por el mapa
        foreach (var kv in map)
        {
            // kv.Key es el indice de la columna en el CSV
            int i = kv.Key;
            // kv.Value es la propiedad de T que corresponde a esa columna
            var prop = kv.Value;

            // Si el indice de la columna es mayor o igual a la cantidad de valores en la linea, se salta (esto no deberia pasar si el formato es correcto). Es un simple check
            if (i >= values.Length)
                continue;

            // Establecemos el valor de la propiedad manualmente en el item creado antes, usando el mapa para obtener la propiedad correcta segun el indice de la columna
            // EJ: Si el mapa tiene 0 => Propiedad Edad, entonces para la columna 0 de la linea, se asigna el valor a la propiedad Edad del item.
            // De esa forma retornara el objeto T ya con los datos de la linea,
            // EJ: Si T es Persona, retorna una Persona con los datos cargados
            var raw = values[i].Trim();
            prop.SetValue(item, ConvertValue(raw, prop.PropertyType));
        }

        return item;
    }

    // Metodo auxiliar para convertir un valor en crudo (string) al tipo de la propiedad correspondiente
    private static object? ConvertValue(string raw, Type propertyType)
    {
        // Elimina espacios extras del valor en crudo
        raw = raw.Trim();

        // Si la propiedad es nullable, obtenemos el tipo subyacente (el tipo real sin el nullable)
        // Ej: Si propertyType es int?, entonces t sera int. Si propertyType es double?, entonces t sera double.
        // Si propertyType no es nullable, entonces t sera el mismo tipo.
        var t = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

        // Si el valor en crudo esta vacio y la propiedad es nullable, retornamos null
        if (raw.Length == 0 && Nullable.GetUnderlyingType(propertyType) != null)
            return null;

        // Si el tipo es string, retornamos el valor en crudo tal cual (sin conversion)
        if (t == typeof(string))
            return raw;

        // Si el tipo es enum, usamos Enum.Parse para convertir el valor en crudo al tipo enum correspondiente
        if (t.IsEnum)
            return Enum.Parse(t, raw, ignoreCase: true);

        // En cualquier otro caso, usamos Convert.ChangeType para convertir el valor en crudo al tipo correspondiente (int, double, decimal, etc)
        return Convert.ChangeType(raw, t, CultureInfo.CurrentCulture);
    }
}
