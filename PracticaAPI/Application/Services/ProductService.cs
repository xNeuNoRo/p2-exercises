using Mapster;
using PracticaAPI.Application.DTOs;
using PracticaAPI.Application.Interfaces.Repositories;
using PracticaAPI.Domain.Entities;
using PracticaAPI.Domain.Exceptions;

namespace PracticaAPI.Application.Services;

public class ProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task CreateProductAsync(CreateProductRequestDto request)
    {
        var product = request.Adapt<Product>();
        await _repo.AddAsync(product);
    }

    public async Task UpdateProductAsync(Guid id, UpdateProductRequestDto request)
    {
        var product = await _repo.GetByIdAsync(id);

        if (product is null)
        {
            throw AppException.NotFound("Producto no encontrado");
        }
        request.Adapt(product);
        await _repo.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        await _repo.DeleteAsync(id);
    }
}
