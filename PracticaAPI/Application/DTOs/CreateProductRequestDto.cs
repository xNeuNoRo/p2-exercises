namespace PracticaAPI.Application.DTOs;

public record CreateProductRequestDto
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required int Stock { get; set; }
}
