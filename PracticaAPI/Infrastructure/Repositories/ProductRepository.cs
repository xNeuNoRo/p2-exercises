using PracticaAPI.Application.Interfaces.Repositories;
using PracticaAPI.Domain.Entities;
using PracticaAPI.Infrastructure.Repositories.Base;

namespace PracticaAPI.Infrastructure.Repositories;

public class ProductRepository : JsonBaseRepo<Product>, IProductRepository
{
    public ProductRepository(string filePath)
        : base(filePath) { }

    public async Task<IEnumerable<Product>> GetAllAsync() => await base.LoadAsync();

    public async Task<Product?> GetByIdAsync(Guid id) => await base.FindAsync(x => x.Id == id);

    public async Task<bool> ExistsAsync(Guid id) => (await base.FindAsync(x => x.Id == id)) != null;

    public async Task<Product> AddAsync(Product product)
    {
        await base.AppendAsync(product);
        return product;
    }

    public async Task<Product?> UpdateAsync(Product product)
    {
        var updatedProduct = await base.UpdateAsync(x => x.Id == product.Id, product);
        return updatedProduct ? product : null;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        // Retorna el resultado de la eliminación de la entidad
        return await base.DeleteAsync(x => x.Id == id);
    }
}
