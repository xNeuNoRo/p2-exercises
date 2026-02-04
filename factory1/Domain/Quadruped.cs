using App.Contracts;

namespace App.Domain;

public class Quadruped : IAnimal
{
    public void PrintHabitat()
    {
        Console.WriteLine(
            "Los cuadrúpedos se pueden encontrar en varios hábitats, incluyendo bosques, praderas y desiertos."
        );
    }

    public override string ToString()
    {
        return "Soy un cuadrúpedo, un animal que camina sobre cuatro patas.";
    }
}
