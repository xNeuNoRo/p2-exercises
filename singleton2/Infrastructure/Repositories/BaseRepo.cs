using System.Text.Json;
using App.Core.Repositories;

namespace App.Infrastructure.Repositories;

// Repositorio base para guardar objetos en archivos de texto (1 JSON por línea)
public abstract class BaseRepo<T>
{
    // Ruta del archivo donde se guardan los datos
    private readonly string _filePath;

    // Constructor que recibe la ruta del archivo y se asegura que exista tanto la carpeta como el archivo
    protected BaseRepo(string filePath)
    {
        // Lo guardamos en la instancia
        _filePath = filePath;

        // Nos aseguramos que la carpeta exista
        var dir = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrWhiteSpace(dir))
            Directory.CreateDirectory(dir);

        // Si el archivo no existe, lo creamos vacío
        if (!File.Exists(_filePath))
            File.WriteAllText(_filePath, "");
    }

    // Lee el archivo y lo convierte a una List<T> (lista generica que asume cualquier tipo especificado)
    public List<T> Load()
    {
        var lines = File.ReadAllLines(_filePath).Where(l => !string.IsNullOrWhiteSpace(l));

        var list = new List<T>();
        foreach (var line in lines)
        {
            // Intentamos deserializar cada linea
            try
            {
                var obj = JsonSerializer.Deserialize<T>(line);
                // Si no es nulo lo agregamos a la lista
                if (obj is not null)
                    list.Add(obj);
            }
            catch
            {
                // Cualquier cosa que falle solo hacemos un log en consola sin hacer ruido de errores en el programa
                Console.WriteLine($"Error deserializing line: {line}");
            }
        }
        return list;
    }

    // Buscar el primer item que cumpla la condición
    public T? Find(Func<T, bool> cb)
    {
        // Cargamos todos los items
        var list = Load();
        // Retornamos el primero que cumpla la condición del cb
        return list.FirstOrDefault(x => cb(x));
    }

    // Guardar una lista de items<T>
    public void SaveAll(List<T> items)
    {
        var lines = items.Select(x => JsonSerializer.Serialize(x)).ToArray();
        File.WriteAllLines(_filePath, lines);
    }

    // Guardar un item al final del txt
    public void Append(T item)
    {
        var line = JsonSerializer.Serialize(item);
        File.AppendAllText(_filePath, line + Environment.NewLine);
    }

    // Borrar items que cumplan la condición
    public bool Delete(Func<T, bool> cb)
    {
        // Cargamos todos los items
        var list = Load();
        // Contamos cuantos habia antes
        int before = list.Count;

        // Filtramos los que no cumplen la con la función de filtrado
        list = list.Where(x => !cb(x)).ToList();
        SaveAll(list);

        return list.Count != before;
    }

    // Actualizar el primer item que cumpla la condición
    public bool Update(Func<T, bool> cb, T newItem)
    {
        // Cargamos todos los items
        var list = Load();
        // Buscamos el índice del primer item que cumpla la condición
        int index = list.FindIndex(x => cb(x));

        // Si no se encontró, retornamos false
        if (index == -1)
            return false;

        // Reemplazamos el item en el índice encontrado
        list[index] = newItem;

        // Guardamos todos los items de nuevo
        SaveAll(list);
        return true;
    }
}
