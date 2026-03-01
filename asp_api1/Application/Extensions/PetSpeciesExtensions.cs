using App.Domain.Enums;

namespace App.Application.Extensions;

public static class PetSpeciesExtensions
{
    // Usamos un switch expression para convertir cada valor del enum PetSpecies a su representación en español
    public static string ToSpanishString(this PetSpecies species) =>
        species switch
        {
            PetSpecies.Dog => "Perro",
            PetSpecies.Cat => "Gato",
            PetSpecies.Rabbit => "Conejo",
            PetSpecies.Hamster => "Hámster",
            PetSpecies.Bird => "Ave",
            PetSpecies.Reptile => "Reptil",
            PetSpecies.Amphibian => "Anfibio",
            PetSpecies.Fish => "Pez",
            PetSpecies.Invertebrate => "Invertebrado",
            PetSpecies.Other => "Otro",
            _ => "Desconocido", // Por si acaso
        };
}
