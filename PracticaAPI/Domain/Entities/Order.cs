using PracticaAPI.Domain.Enums;

namespace PracticaAPI.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderProducts> Products { get; set; } = new();
    public decimal ITBIS => Total * 0.18m;
    public decimal Total => Products.Sum(p => p.Subtotal);
}
