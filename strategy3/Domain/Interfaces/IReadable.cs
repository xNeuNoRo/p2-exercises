namespace App.Domain.Interfaces;

public interface IReadable<T>
    where T : class
{
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T?> GetByIdAsync(int id);
    public Task<bool> ExistsAsync(int id);
}
