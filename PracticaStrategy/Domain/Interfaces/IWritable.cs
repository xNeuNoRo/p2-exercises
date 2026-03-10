namespace App.Domain.Interfaces;

public interface IWritable<T>
    where T : class
{
    public Task<T> AddAsync(T entity);
    public Task<T?> UpdateAsync(T entity);
}
