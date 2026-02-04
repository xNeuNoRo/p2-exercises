using App.Contracts;

namespace App.Domain;

public class Fish : IAnimal
{
    public void PrintHabitat()
    {
        Console.WriteLine(
            "Los peces se encuentran principalmente en hábitats acuáticos como océanos, ríos y lagos."
        );
    }

    public override string ToString()
    {
        return "Soy un pez, un animal que vive en el agua.";
    }
}
