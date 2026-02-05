using System.Text.Json;

namespace App.Infrastructure.Repositories;

public class JsonRepo<T>
    where T : class, new() // Restringimos T para que sea una clase que tenga un constructor sin parametros
// Ya que de esa forma podemos crear instancias de T dentro de la clase y asignar manualmente sus propiedades 1 por 1
{
    // Ruta del archivo JSON
    private readonly string _filePath;

    // Opciones de serializacion JSON
    private readonly JsonSerializerOptions _options;

    // SINGLETON
    private static JsonRepo<T>? _instance;

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
        };

        // Si el archivo no existe, lo crea con un array vacío
        if (!File.Exists(_filePath))
        {
            // Creamos un archivo JSON inicial con un array vacío
            File.WriteAllText(_filePath, "[]");
        }
    }

    // Metodo para obtener la instancia singleton
    public static JsonRepo<T> GetInstance(string filePath)
    {
        if (_instance == null)
        {
            _instance = new JsonRepo<T>(filePath);
        }

        return _instance;
    }

    // Metodo para cargar todos los items del archivo
    public List<T> Load()
    {
        // Si el archivo no existe, retorna una lista vacia
        if (!File.Exists(_filePath))
            return new List<T>();

        // Lee el contenido del archivo JSON
        string json = File.ReadAllText(_filePath);

        // Si el contenido esta vacío, retorna una lista vacia
        if (string.IsNullOrWhiteSpace(json))
            return new List<T>();

        // Deserializamos el contenido completo a una lista de objetos T
        // Si la deserializacion falla por X o Y razon, retornamos una lista vacia
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }

    // Metodo para guardar todos los items en el archivo (sobrescribe lo que haya)
    public void SaveAll(List<T> items)
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
        var items = Load();

        // Agregamos el nuevo item a la lista
        items.Add(item);

        // Guardamos nuevamente toda la lista en el archivo
        SaveAll(items);
    }

    // Metodo para encontrar un item que cumpla con el callback dado.
    // Ej: x => x.Id == 5 (busca el item con Id 5)
    public T? Find(Func<T, bool> cb)
    {
        // Buscamos el primer objeto que cumpla la condición del delegado
        return Load().FirstOrDefault(cb);
    }

    // Metodo para actualizar un item que cumpla con el callback dado
    public bool Update(Func<T, bool> cb, T newItem)
    {
        // Cargamos todos los items
        var items = Load();

        // Buscamos el indice del primer item que cumple con el callback
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
        // Cargamos todos los items
        var items = Load();

        // Cuenta cuantos items habia inicialmente
        int initialCount = items.Count;

        // Filtramos los items que NO cumplen con el callback
        // EJ: Si cb es x => x.Id == 5, se quedan todos los que NO tienen Id 5
        var remainingItems = items.Where(x => !cb(x)).ToList();

        // Si la cantidad de items restantes es diferente a la inicial, significa que se elimino al menos uno
        if (remainingItems.Count != initialCount)
        {
            // Guardamos la lista actualizada sin los items eliminados
            SaveAll(remainingItems);
            return true;
        }

        // Retornamos false indicando que no se elimino ningun item
        return false;
    }
}
