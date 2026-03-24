using PracticaAPI.Domain.Enums;

namespace PracticaAPI.Application.DTOs;

public record UpdateOrderRequestDto
{
    public OrderStatus Status { get; set; }
}
