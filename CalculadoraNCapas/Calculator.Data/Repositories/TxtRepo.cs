using System.Collections.Concurrent;
using System.Globalization;
using System.Reflection;
using Calculator.Data.Contracts;

namespace Calculator.Data.Repositories;

public class TxtRepo<T> : ITxtRepo<T>
    where T : class, new() // Restringimos T para que sea una clase que tenga un constructor sin parametros
// Ya que de esa forma podemos crear instancias de T dentro de la clase y asignar manualmente sus propiedades 1 por 1
{
    // Campos requeridos para el manejo de archivos de texto
    private readonly string _filePath; // Ruta del archivo de texto
    private readonly PropertyInfo[] _properties; // Propiedades de la clase T
    private const string BlockSeparator = "------"; // Separador entre bloques de datos

    // SINGLETON POR ARCHIVO (O TABLA)
    // Diccionario concurrente para almacenar las instancias de TxtRepo<T> por ruta de archivo, garantizando que solo haya una instancia por archivo (o tabla basicamente ya que simula una tabla sql)
    // El diccionario concurrente es igual que un diccionario normal pero con la seguridad de que es atomico
    private static readonly ConcurrentDictionary<string, TxtRepo<T>> _instances = new();

    // Constructor que inicializa la ruta del archivo y las propiedades de T
    private TxtRepo(string filePath)
    {
        _filePath = filePath; // Inicializa la ruta del archivo
        // Con typeof(T) obtenemos la declaracion de la clase T (basicamente como moldeamos la clase)
        // Y GetProperties() obtiene todas sus propiedades
        _properties = typeof(T).GetProperties();
        if (!File.Exists(_filePath)) // Si el archivo no existe, lo crea
            File.WriteAllText(_filePath, "");
    }

    // Metodo para obtener la instancia unica de TxtRepo<T> para una ruta de archivo dada
    public static TxtRepo<T> GetInstance(string filePath) =>
        _instances.GetOrAdd(filePath, fp => new TxtRepo<T>(fp));

    // Metodo para agregar un nuevo item al archivo
    public void Append(T item) => File.AppendAllText(_filePath, ToBlock(item));

    public void SaveAll(List<T> items)
    {
        // [items.Select(ToBlock)] => Convierte cada item a bloque de texto usando el metodo ToBlock
        // [string.Join("", ...)] => Une todos los bloques en un solo string
        var content = string.Join("", items.Select(ToBlock));
        // Finalmente guardamos el contenido en el archivo
        File.WriteAllText(_filePath, content);
    }

    // Metodo para cargar todos los items del archivo
    public List<T> Load()
    {
        // Si el archivo no existe, retorna una lista vacia
        if (!File.Exists(_filePath))
            return new List<T>();

        // Lee el contenido del archivo
        var content = File.ReadAllText(_filePath);

        // Divide el contenido en bloques usando el separador definido
        // El StringSplitOptions.RemoveEmptyEntries evita bloques vacios, por si hay saltos de linea extras
        var blocks = content
            .Split([BlockSeparator], StringSplitOptions.RemoveEmptyEntries)
            .Select(b => b.Trim()) // Elimina espacios extras al inicio y al final de cada bloque
            .Where(b => b.Length > 0); // Filtra bloques vacios, por si el archivo tiene saltos de linea extras al final
        // Por cada bloque, lo parsea a un objeto T usando el metodo ParseBlock y retorna la lista de objetos
        return blocks.Select(ParseBlock).ToList();
    }

    // Metodo para encontrar un item que cumpla con callback dado. Ej: x => x.Id == 5 (busca el item con Id 5)
    public T? Find(Func<T, bool> cb) => Load().FirstOrDefault(cb);

    // Metodo para actualizar un item que cumpla con el callback dado
    public bool Update(Func<T, bool> cb, T newItem)
    {
        // Carga todos los items
        var items = Load();

        // Busca el indice del primer item que cumple con el callback
        int index = items.FindIndex(new Predicate<T>(cb));

        // Si no se encontro ningun item que cumpla con el callback, retorna false
        if (index == -1)
            return false;

        // Reemplaza el item en el indice encontrado con el nuevo item dado
        items[index] = newItem;

        // Guarda todos los items actualizados en el archivo
        SaveAll(items);

        // Retorna true indicando que se actualizo correctamente
        return true;
    }

    // Metodo para eliminar items que cumplan con el callback dado
    public bool Delete(Func<T, bool> cb)
    {
        // Carga todos los items
        var items = Load();
        // Cuenta cuantos items habia inicialmente
        int initialCount = items.Count;

        // Filtra los items, dejando solo los que NO cumplen con el callback
        // Ej: Si cb es x => x.Id == 5, se quedan todos los items que NO tengan Id 5
        items = items.Where(x => !cb(x)).ToList();

        // Si la cantidad de items cambio, significa que se elimino al menos uno
        if (items.Count != initialCount)
        {
            // Guarda la lista actualizada sin los items eliminados
            SaveAll(items);
            return true;
        }

        // Si no se elimino nada, retorna false
        return false;
    }

    // Convierte un objeto T a un bloque de texto
    private string ToBlock(T item)
    {
        // Aprovechando las propiedades obtenidas en el constructor,
        // se crea una linea por cada propiedad en el formato "NombrePropiedad: ValorPropiedad"
        var lines = _properties.Select(p => $"{p.Name}: {p.GetValue(item)}");

        // Une todas las lineas con saltos de linea y agrega el separador al final
        return string.Join("\n", lines) + $"\n{BlockSeparator}\n";
    }

    // Parsea un bloque de texto a un objeto T
    private T ParseBlock(string block)
    {
        // Crea una nueva instancia de T vacia
        var item = new T();
        // Divide el bloque en lineas por saltos de linea
        var lines = block.Trim().Split('\n');

        // Por cada linea, procesamos y asignamos el valor a la propiedad correspondiente
        foreach (var line in lines)
        {
            // Ya sabemos previamente que se debe cumplir el formato (NombrePropiedad: ValorPropiedad)
            // Entonces separamos por los dos puntos, limitando a 2 partes
            var parts = line.Split(':', 2);

            // Si no hay al menos 2 partes, se salta la linea (esto no deberia pasar si el formato es correcto)
            if (parts.Length < 2)
                continue;

            // Busca la propiedad correspondiente por nombre
            var prop = _properties.FirstOrDefault(p => p.Name == parts[0].Trim());

            // Extraemos el valor de la propiedad manualmente en el item creado antes
            // Usamos Trim() para eliminar espacios extras
            var rawValue = parts[1].Trim();

            // Si la propiedad existe, convertimos el valor en crudo al tipo de la propiedad y lo asignamos
            prop?.SetValue(item, ConvertValue(rawValue, prop.PropertyType));
        }

        // Retorna el objeto T ya con los datos del bloque,
        // EJ: Si T es Persona, retorna una Persona con los datos cargados
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
