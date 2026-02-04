using App.Contracts;

namespace App.Domain;

public class Bird : IAnimal
{
    public void PrintHabitat()
    {
        Console.WriteLine(
            "Las aves se encuentran en una variedad de hábitats, incluyendo bosques, praderas y áreas urbanas."
        );
    }

    public override string ToString()
    {
        return "Soy un ave, un animal que puede volar.";
    }
}
