using PracticaAPI.Domain.Entities;

namespace PracticaAPI.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<Product> AddAsync(Product product);
    Task<Product?> UpdateAsync(Product product);
    Task<bool> DeleteAsync(Guid id);
}
