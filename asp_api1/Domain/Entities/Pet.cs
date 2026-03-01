using App.Domain.Enums;

namespace App.Domain.Entities;

// Clase que representara a una mascota en la app
public class Pet
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required PetSpecies Species { get; set; }
    public required string Race { get; set; }
    public int Age { get; set; }
}
