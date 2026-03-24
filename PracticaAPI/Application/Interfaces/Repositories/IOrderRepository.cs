using PracticaAPI.Domain.Entities;

namespace PracticaAPI.Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<Order> AddAsync(Order order);
    Task<Order?> UpdateAsync(Order order);
    Task<bool> DeleteAsync(Guid id);
}
