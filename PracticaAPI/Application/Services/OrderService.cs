using PracticaAPI.Application.DTOs;
using PracticaAPI.Application.Interfaces.Repositories;
using PracticaAPI.Domain.Entities;
using PracticaAPI.Domain.Enums;
using PracticaAPI.Domain.Exceptions;

namespace PracticaAPI.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepo;
    private readonly IProductRepository _productRepo;

    public OrderService(IOrderRepository orderRepo, IProductRepository productRepo)
    {
        _orderRepo = orderRepo;
        _productRepo = productRepo;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _orderRepo.GetAllAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(Guid id)
    {
        return await _orderRepo.GetByIdAsync(id);
    }

    public async Task<Order> CreateOrderAsync(CreateOrderRequestDto request)
    {
        var order = new Order { Status = request.Status };
        await _orderRepo.AddAsync(order);
        return order;
    }

    public async Task UpdateOrderAsync(Guid orderId, UpdateOrderRequestDto request)
    {
        var existingOrder = await _orderRepo.GetByIdAsync(orderId);

        if (existingOrder is null)
        {
            throw AppException.NotFound("Orden no encontrada");
        }

        if (existingOrder.Status == OrderStatus.Cancelled)
        {
            throw AppException.BadRequest(
                "No se pueden modificar órdenes canceladas",
                ErrorCodes.BadRequest
            );
        }

        existingOrder.Status = request.Status;

        await _orderRepo.UpdateAsync(existingOrder);
    }

    public async Task DeleteOrderAsync(Guid id)
    {
        await _orderRepo.DeleteAsync(id);
    }

    // Logica de negocio

    public async Task CancelOrderAsync(Guid orderId)
    {
        var order = await _orderRepo.GetByIdAsync(orderId);

        if (order is null)
        {
            throw AppException.NotFound("Orden no encontrada");
        }

        order.Status = OrderStatus.Cancelled;

        foreach (var item in order.Products)
        {
            var product = await _productRepo.GetByIdAsync(item.Product.Id);
            if (product is not null)
            {
                product.Stock += item.Quantity; // Devolver el stock al producto
                await _productRepo.UpdateAsync(product);
            }
        }

        await _orderRepo.UpdateAsync(order);
    }

    public async Task CompleteOrderAsync(Guid orderId)
    {
        var order = await _orderRepo.GetByIdAsync(orderId);

        if (order is null)
        {
            throw AppException.NotFound("Orden no encontrada");
        }

        if (order.Products.Count == 0)
        {
            throw AppException.BadRequest(
                "No se pueden completar órdenes sin productos",
                ErrorCodes.BadRequest
            );
        }

        order.Status = OrderStatus.Completed;

        await _orderRepo.UpdateAsync(order);
    }

    public async Task AddProductToOrderAsync(Guid orderId, Guid productId, int quantity)
    {
        var order = await _orderRepo.GetByIdAsync(orderId);
        var product = await _productRepo.GetByIdAsync(productId);

        if (order is null || product is null)
        {
            throw AppException.NotFound("Orden o producto no encontrado");
        }

        if (order.Status == OrderStatus.Cancelled)
        {
            throw AppException.BadRequest(
                "No se pueden modificar órdenes canceladas",
                ErrorCodes.BadRequest
            );
        }

        // Regla: No permitir cantidades menores o iguales a 0
        if (quantity <= 0)
        {
            throw AppException.BadRequest("La cantidad debe ser mayor a 0", ErrorCodes.BadRequest);
        }

        if (product.Stock < quantity)
        {
            throw AppException.BadRequest(
                "No hay suficiente stock para agregar el producto a la orden",
                ErrorCodes.BadRequest
            );
        }

        // Si el producto ya existe en el pedido no se duplica, se incrementa la cantidad
        var existingOrderItem = order.Products.FirstOrDefault(p => p.Product.Id == productId);

        if (existingOrderItem != null)
        {
            existingOrderItem.Quantity += quantity;
        }
        else
        {
            var orderItem = new OrderProducts { Product = product, Quantity = quantity };
            order.Products.Add(orderItem);
        }

        product.Stock -= quantity;

        await _orderRepo.UpdateAsync(order);
        await _productRepo.UpdateAsync(product);
    }

    public async Task RemoveProductFromOrderAsync(Guid orderId, Guid productId)
    {
        var order = await _orderRepo.GetByIdAsync(orderId);
        var product = await _productRepo.GetByIdAsync(productId);

        if (order is null || product is null)
        {
            throw AppException.NotFound("Orden o producto no encontrado");
        }

        if (order.Status == OrderStatus.Cancelled)
        {
            throw AppException.BadRequest(
                "No se pueden modificar órdenes canceladas",
                ErrorCodes.BadRequest
            );
        }

        var orderItem = order.Products.FirstOrDefault(p => p.Product.Id == productId);

        if (orderItem is null)
        {
            throw AppException.NotFound("Producto no encontrado en la orden");
        }

        product.Stock += orderItem.Quantity; // Devolver el stock al producto
        order.Products.Remove(orderItem);

        await _orderRepo.UpdateAsync(order);
        await _productRepo.UpdateAsync(product);
    }

    public async Task IncreaseOrderProductQuantityAsync(Guid orderId, Guid productId, int quantity)
    {
        var order = await _orderRepo.GetByIdAsync(orderId);
        var product = await _productRepo.GetByIdAsync(productId);

        if (order is null || product is null)
        {
            throw AppException.NotFound("Orden o producto no encontrado");
        }

        if (order.Status == OrderStatus.Cancelled)
        {
            throw AppException.BadRequest(
                "No se pueden modificar órdenes canceladas",
                ErrorCodes.BadRequest
            );
        }

        var orderItem = order.Products.FirstOrDefault(p => p.Product.Id == productId);

        if (orderItem is null)
        {
            throw AppException.NotFound("Producto no encontrado en la orden");
        }

        if (product.Stock < quantity)
        {
            throw AppException.BadRequest(
                "No hay suficiente stock para aumentar la cantidad",
                ErrorCodes.BadRequest
            );
        }

        product.Stock -= quantity;
        orderItem.Quantity += quantity;

        await _orderRepo.UpdateAsync(order);
        await _productRepo.UpdateAsync(product);
    }

    public async Task DecreaseOrderProductQuantityAsync(Guid orderId, Guid productId, int quantity)
    {
        var order = await _orderRepo.GetByIdAsync(orderId);

        if (order is null)
        {
            throw AppException.NotFound("Orden no encontrada");
        }

        if (order.Status == OrderStatus.Cancelled)
        {
            throw AppException.BadRequest(
                "No se pueden modificar órdenes canceladas",
                ErrorCodes.BadRequest
            );
        }

        var orderItem = order.Products.FirstOrDefault(p => p.Product.Id == productId);

        if (orderItem is null)
        {
            throw AppException.NotFound("Producto no encontrado en la orden");
        }

        if (orderItem.Quantity < quantity)
        {
            throw AppException.BadRequest(
                "La cantidad a disminuir es mayor que la cantidad actual",
                ErrorCodes.BadRequest
            );
        }

        orderItem.Quantity -= quantity;

        await _orderRepo.UpdateAsync(order);
    }
}
