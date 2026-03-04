using App.Domain.Enums;
using App.Domain.Interfaces;
using App.Domain.Strategies;

namespace App.Domain.Factories;

public class PetWalkerStrategyFactory
{
    protected PetWalkerStrategyFactory() { }

    public static IPetWalkerStrategy GetStrategy(PetSpecies species)
    {
        return species switch
        {
            PetSpecies.Chihuahua => new ChihuahuaStrategy(),
            PetSpecies.Labrador => new LabradorStrategy(),
            PetSpecies.Husky => new HuskyStrategy(),
            _ => throw new ArgumentException("Especie no soportada"),
        };
    }
}
