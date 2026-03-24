using Mapster;
using Microsoft.AspNetCore.Mvc;
using PracticaAPI.Application.DTOs;
using PracticaAPI.Application.Services;
using PracticaAPI.Controllers.Base;
using PracticaAPI.Domain.Entities;

namespace PracticaAPI.Controllers;

public class ProductController : BaseApiController
{
    private readonly ProductService _service;

    public ProductController(ProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetAllProductsAsync();
        return Success(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _service.GetProductByIdAsync(id);
        return Success(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequestDto request)
    {
        var createdProduct = _service.CreateProductAsync(request);
        return Success(createdProduct);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequestDto request)
    {
        await _service.UpdateProductAsync(id, request);
        return Success();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteProductAsync(id);
        return Success();
    }
}
