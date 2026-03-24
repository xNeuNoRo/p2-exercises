using PracticaAPI.Application.Interfaces.Repositories;
using PracticaAPI.Domain.Entities;
using PracticaAPI.Infrastructure.Repositories.Base;

namespace PracticaAPI.Infrastructure.Repositories;

public class OrderRepository : JsonBaseRepo<Order>, IOrderRepository
{
    public OrderRepository(string filePath)
        : base(filePath) { }

    public async Task<IEnumerable<Order>> GetAllAsync() => await base.LoadAsync();

    public async Task<Order?> GetByIdAsync(Guid id) => await base.FindAsync(x => x.Id == id);

    public async Task<bool> ExistsAsync(Guid id) => (await base.FindAsync(x => x.Id == id)) != null;

    public async Task<Order> AddAsync(Order order)
    {
        await base.AppendAsync(order);
        return order;
    }

    public async Task<Order?> UpdateAsync(Order order)
    {
        var updatedOrder = await base.UpdateAsync(x => x.Id == order.Id, order);
        return updatedOrder ? order : null;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        // Retorna el resultado de la eliminación de la entidad
        return await base.DeleteAsync(x => x.Id == id);
    }
}
