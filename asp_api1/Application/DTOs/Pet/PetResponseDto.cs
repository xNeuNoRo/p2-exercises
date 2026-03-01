using App.Application.Extensions;
using App.Domain.Enums;

namespace App.Application.DTOs.Pet;

public record PetResponseDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required PetSpecies SpeciesValue { get; init; }
    public string Species => SpeciesValue.ToSpanishString();
    public required string Race { get; init; }
    public int Age { get; init; }
}
