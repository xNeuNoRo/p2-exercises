using Mapster;
using Microsoft.AspNetCore.Mvc;
using PracticaAPI.Application.DTOs;
using PracticaAPI.Application.Services;
using PracticaAPI.Controllers.Base;
using PracticaAPI.Domain.Entities;

namespace PracticaAPI.Controllers;

public class OrderController : BaseApiController
{
    private readonly OrderService _service;

    public OrderController(OrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _service.GetAllOrdersAsync();
        return Success(orders);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await _service.GetOrderByIdAsync(id);
        return Success(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequestDto request)
    {
        var order = await _service.CreateOrderAsync(request);
        return Success(order);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrderRequestDto request)
    {
        await _service.UpdateOrderAsync(id, request);
        return Success();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteOrderAsync(id);
        return Success();
    }

    // Logica de negocio adicional

    [HttpPost("{orderId:guid}/add-product/{productId:guid}")]
    public async Task<IActionResult> AddProduct(
        [FromRoute] Guid orderId,
        [FromRoute] Guid productId,
        [FromBody] int quantity
    )
    {
        await _service.AddProductToOrderAsync(orderId, productId, quantity);
        return Success();
    }

    [HttpDelete("{orderId:guid}/product/{productId:guid}")]
    public async Task<IActionResult> RemoveProduct(
        [FromRoute] Guid orderId,
        [FromRoute] Guid productId
    )
    {
        await _service.RemoveProductFromOrderAsync(orderId, productId);
        return Success();
    }

    [HttpPatch("{orderId:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid orderId)
    {
        await _service.CancelOrderAsync(orderId);
        return Success();
    }

    [HttpPatch("{orderId:guid}/complete")]
    public async Task<IActionResult> Complete(Guid orderId)
    {
        await _service.CompleteOrderAsync(orderId);
        return Success();
    }

    [HttpPost("{orderId:guid}/increase-product/{productId:guid}")]
    public async Task<IActionResult> IncreaseProductQuantity(
        [FromRoute] Guid orderId,
        [FromRoute] Guid productId,
        [FromBody] int quantity
    )
    {
        await _service.IncreaseOrderProductQuantityAsync(orderId, productId, quantity);
        return Success();
    }

    [HttpPost("{orderId:guid}/decrease-product/{productId:guid}")]
    public async Task<IActionResult> DecreaseProductQuantity(
        [FromRoute] Guid orderId,
        [FromRoute] Guid productId,
        [FromBody] int quantity
    )
    {
        await _service.DecreaseOrderProductQuantityAsync(orderId, productId, quantity);
        return Success();
    }
}
