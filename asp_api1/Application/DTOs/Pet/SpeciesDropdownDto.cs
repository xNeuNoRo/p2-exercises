namespace App.Application.DTOs.Pet;

public record SpeciesDropdownDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
}
