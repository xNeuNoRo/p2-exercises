using App.Domain.Enums;

namespace App.Application.DTOs.Base;

public abstract record PetRequestBase
{
    public required string Name { get; init; }
    public required string Race { get; init; }
    public required PetSpecies Species { get; init; }
    public int Age { get; init; }
}
