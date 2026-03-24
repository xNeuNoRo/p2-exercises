using PracticaAPI.Domain.Enums;

namespace PracticaAPI.Application.DTOs;

public record CreateOrderRequestDto
{
    public OrderStatus Status { get; set; }
}
