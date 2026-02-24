using System.Collections.Concurrent;
using System.Text.Json;
using App.Domain.Contracts;

namespace App.Infrastructure.Repositories;

public class JsonRepo<T> : IFileRepo<T>
    where T : class, new() // Restringimos T para que sea una clase que tenga un constructor sin parametros
// Ya que de esa forma podemos crear instancias de T dentro de la clase y asignar manualmente sus propiedades 1 por 1
{
    // Ruta del archivo JSON
    private readonly string _filePath;

    // Opciones de serializacion JSON
    private readonly JsonSerializerOptions _options;

    // SINGLETON POR ARCHIVO (O TABLA)
    // Diccionario concurrente para almacenar las instancias de JsonRepo<T> por ruta de archivo, garantizando que solo haya una instancia por archivo (o tabla basicamente ya que simula una tabla sql)
    // El diccionario concurrente es igual que un diccionario normal pero con la seguridad de que es atomico
    private static readonly ConcurrentDictionary<string, JsonRepo<T>> _instances = new();

    // Constructor que inicializa la ruta del archivo y las opciones de serializacion
    private JsonRepo(string filePath)
    {
        // Inicializa la ruta del archivo
        _filePath = filePath;
        // Configuramos las opciones para que el JSON sea indentado (mas legible)
        _options = new JsonSerializerOptions
        {
            WriteIndented = true, // Indenta el JSON para que sea mas legible
            PropertyNameCaseInsensitive = true, // Ignora mayusculas/minusculas en los nombres de las propiedades
            AllowTrailingCommas = true, // Permite comas al final de los objetos/arrays en el JSON
        };

        // Asegura que el archivo exista
        EnsureFile();
    }

    // Metodo para asegurar que el archivo exista y tenga un array vacío si esta vacio
    private void EnsureFile()
    {
        // Si el archivo no existe o esta vacio, lo crea con un array vacío
        if (!File.Exists(_filePath) || new FileInfo(_filePath).Length == 0)
            File.WriteAllText(_filePath, "[]");
    }

    // Metodo para obtener la instancia unica de JsonRepo<T> para una ruta de archivo dada
    public static JsonRepo<T> GetInstance(string filePath) =>
        _instances.GetOrAdd(filePath, fp => new JsonRepo<T>(fp));

    // Metodo para cargar todos los items del archivo
    public List<T> ReadData()
    {
        // Asegura que el archivo exista antes de intentar cargarlo
        EnsureFile();

        // Lee el contenido del archivo JSON
        string json = File.ReadAllText(_filePath);

        // Si el contenido esta vacío, retorna una lista vacia
        if (string.IsNullOrWhiteSpace(json))
            return new List<T>();

        try
        {
            // Deserializamos el contenido completo a una lista de objetos T
            // Si la deserializacion falla por X o Y razon, retornamos una lista vacia
            return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new List<T>();
        }
        catch (JsonException)
        {
            // Si el JSON se corrompio, no se puede deserializar, o algo raro idk, retornamos una lista vacia
            return new List<T>();
        }
    }

    // Metodo para guardar todos los items en el archivo (sobrescribe lo que haya)
    public void SaveData(List<T> items)
    {
        // Serializamos la lista completa a formato JSON
        string json = JsonSerializer.Serialize(items, _options);
        // Escribimos el JSON en el archivo (sobrescribiendo lo que haya)
        File.WriteAllText(_filePath, json);
    }

    // Metodo para agregar un nuevo item al archivo
    public void Append(T item)
    {
        // Cargamos todos los items existentes
        var items = ReadData();

        // Agregamos el nuevo item a la lista
        items.Add(item);

        // Guardamos nuevamente toda la lista en el archivo
        SaveData(items);
    }

    // Metodo para encontrar un item que cumpla con el callback dado.
    // Ej: x => x.Id == 5 (busca el item con Id 5)
    public T? Find(Func<T, bool> cb)
    {
        // Buscamos el primer objeto que cumpla la condición del delegado
        return ReadData().FirstOrDefault(cb);
    }
}
