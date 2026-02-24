using App.Domain.Contracts;
using App.Infrastructure.Repositories;

namespace App.Infrastructure.Adapters;

public class CsvRepoAdapter<T> : IFileRepo<T>
    where T : class, new() // Restringimos T para que sea una clase que tenga un constructor sin parametros
{
    private readonly CsvRepo<T> _csvRepo;

    public CsvRepoAdapter(string filePath)
    {
        // Obtenemos la instancia del CsvRepo<T> para la ruta de archivo dada
        _csvRepo = CsvRepo<T>.GetInstance(filePath);
    }

    // Implementación de los métodos de IFileRepo<T>
    // delegando a los métodos correspondientes de CsvRepo<T>

    // Metodo ReadData() que es el equivalente de GetCsv() en CsvRepo<T>
    public List<T> ReadData()
    {
        return _csvRepo.GetCsv();
    }

    // Metodo SaveData() que es el equivalente de SaveCsv() en CsvRepo<T>
    public void SaveData(List<T> items)
    {
        _csvRepo.SaveCsv(items);
    }

    // Metodo Append() que es el equivalente de AppendItem() en CsvRepo<T>
    public void Append(T item)
    {
        _csvRepo.AppendItem(item);
    }

    // Metodo Find() que es el equivalente de FindItem() en CsvRepo<T>
    public T? Find(Func<T, bool> cb)
    {
        return _csvRepo.FindItem(cb);
    }
}
