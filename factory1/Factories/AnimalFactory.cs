using App.Contracts;
using App.Domain;

namespace App.Factories;

public class AnimalFactory
{
    protected AnimalFactory() { }

    public static IAnimal Create(HabitatType habitat)
    {
        return habitat switch
        {
            HabitatType.Oceans => new Fish(),
            HabitatType.Forests => new Bird(),
            HabitatType.Deserts => new Quadruped(),
            _ => throw new ArgumentException("Hábitat no soportado"),
        };
    }
}
