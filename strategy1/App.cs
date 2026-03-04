using App.Domain.Enums;
using App.Domain.Factories;
using App.Domain.Services;
using App.Helpers;

namespace App;

public class StrategyApp
{
    public StrategyApp() { }

    public void Run()
    {
        bool loop = true;
        while (loop)
        {
            var choice = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Strategy Pattern\nDeveloped by Angel G.M",
                    Choices = ["Pasear un chihuahua", "Pasear un labrador", "Pasear un Husky"],
                }
            );

            PetSpecies species = choice switch
            {
                0 => PetSpecies.Chihuahua,
                1 => PetSpecies.Labrador,
                2 => PetSpecies.Husky,
                _ => throw new ArgumentException("Especie no soportada"),
            };

            var (price, minutes) = new PetWalkerCalculator(
                PetWalkerStrategyFactory.GetStrategy(species)
            ).GetPlan();

            Console.WriteLine(
                $"Especie {species}\nPrecio del paseo: {price}, Duración en minutos: {minutes}"
            );
            Console.WriteLine("\nPresiona [Enter] para continuar...");
            Console.ReadLine();
        }
    }
}
