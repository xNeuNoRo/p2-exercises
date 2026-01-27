namespace App.Core.Repositories;

public interface IRepository<T, TKey>
{
    IReadOnlyList<T> GetAll();
    T? GetById(TKey id);

    void Add(T entity);
    bool Update(T entity);
    bool Delete(TKey id);
}
