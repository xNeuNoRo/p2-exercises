using System.Reflection;

namespace App.Infrastructure.Repositories;

public class CsvRepo<T>
    where T : class, new() // Restringimos T para que sea una clase que tenga un constructor sin parametros
// Ya que de esa forma podemos crear instancias de T dentro de la clase y asignar manualmente sus propiedades 1 por 1
{
    // Campos requeridos para el manejo de archivos CSV
    private readonly string _filePath; // Ruta del archivo CSV
    private readonly PropertyInfo[] _properties; // Propiedades de la clase T
    private const string Separator = "|"; // Separador entre valores en el CSV

    // SINGLETON
    private static CsvRepo<T>? _instance;

    // Constructor que inicializa la ruta del archivo y las propiedades de T
    private CsvRepo(string filePath)
    {
        _filePath = filePath; // Inicializa la ruta del archivo
        // Con typeof(T) obtenemos todos los detalles de la clase T (basicamente como moldeamos la clase y mas datos)
        // Y GetProperties() obtiene todas sus propiedades
        _properties = typeof(T).GetProperties();
        if (!File.Exists(_filePath)) // Si el archivo no existe, lo crea
            WriteHeader();
    }

    // Metodo para obtener la instancia singleton
    public static CsvRepo<T> GetInstance(string filePath)
    {
        if (_instance == null)
        {
            _instance = new CsvRepo<T>(filePath);
        }

        return _instance;
    }

    // Metodo para escribir los headers del CSV con los nombres de las propiedades de T
    private void WriteHeader()
    {
        // Crea una linea con los nombres de las propiedades separadas por el separador definido
        var header = string.Join(Separator, _properties.Select(p => p.Name));
        // Escribe la linea de header en el archivo (o sobreescribe si ya existe)
        File.WriteAllText(_filePath, header + Environment.NewLine);
        // EJ: Si T es Persona con propiedades Nombre y Edad,
        // el archivo CSV tendra la primera linea: "Nombre|Edad"
    }

    // Metodo para agregar un nuevo item al archivo
    public void Append(T item) => File.AppendAllText(_filePath, ToLine(item) + "\n");

    // Metodo para guardar todos los items en el archivo (sobrescribe lo que haya)
    public void SaveAll(List<T> items)
    {
        // Primero escribe el header
        WriteHeader();
        // Luego agrega todas las lineas correspondientes a los items
        File.AppendAllLines(_filePath, items.Select(ToLine));
    }

    // Metodo para cargar todos los items del archivo
    public List<T> Load()
    {
        // Si el archivo no existe, retorna una lista vacia
        if (!File.Exists(_filePath))
            return new List<T>();

        // Lee todas las lineas del archivo, omitiendo la primera (header)
        var lines = File.ReadAllLines(_filePath).Skip(1); // Skip Header
        // Por cada linea, la parsea a un objeto T usando el metodo ParseLine y retorna la lista de objetos
        return lines.Select(ParseLine).ToList();
    }

    // Metodo para encontrar un item que cumpla con el callback dado. Ej: x => x.Id == 5 (busca el item con Id 5)
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
        var remainingItems = items.Where(x => !cb(x)).ToList();

        // Si la cantidad de items cambio, significa que se elimino al menos uno
        if (remainingItems.Count != initialCount)
        {
            // Guarda la lista actualizada sin los items eliminados
            SaveAll(remainingItems);
            // Retornamos true indicando que se elimino al menos un item
            return true;
        }

        // Si no se elimino nada, retorna false
        return false;
    }

    // Convierte un objeto T a una linea de texto en formato CSV
    private string ToLine(T item) =>
        string.Join(Separator, _properties.Select(p => p.GetValue(item)));

    // Convierte un objeto T a una linea de texto en formato CSV
    private T ParseLine(string line)
    {
        // Crea una nueva instancia de T vacia
        var item = new T();

        // Divide la linea por el separador definido
        var values = line.Split(Separator);

        // Asigna cada valor a la propiedad correspondiente en el objeto T
        for (int i = 0; i < _properties.Length && i < values.Length; i++)
        {
            // Establecemos el valor de la propiedad manualmente en el item creado antes
            _properties[i].SetValue(item, values[i]);
        }

        // Retorna el objeto T ya con los datos de la linea,
        // EJ: Si T es Persona, retorna una Persona con los datos cargados
        return item;
    }
}
