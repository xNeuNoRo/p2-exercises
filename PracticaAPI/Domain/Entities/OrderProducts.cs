namespace PracticaAPI.Domain.Entities;

public class OrderProducts
{
    public required Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal => Product.Price * Quantity;
}