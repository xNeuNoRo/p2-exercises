namespace Calculator.Data.Contracts;

public interface ITxtRepo<T>
    where T : class, new() // Restringimos T para que sea una clase que tenga un constructor sin parametros
{
    void Append(T item);
    void SaveAll(List<T> items);
    List<T> Load();
    T? Find(Func<T, bool> cb);
    bool Update(Func<T, bool> cb, T newItem);
    bool Delete(Func<T, bool> cb);
}
