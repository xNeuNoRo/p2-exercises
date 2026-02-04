using App.Domain;
using App.Factories;
using App.Helpers;

namespace App;

public class AnimalApp
{
    public AnimalApp() { }

    private static void PressEnterToContinue()
    {
        Console.WriteLine("\nPresiona [Enter] para continuar...");
        Console.ReadLine();
    }

    public void Run()
    {
        bool loop = true;
        while (loop)
        {
            int choice = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Seleccione un hábitat para crear un animal:",
                    Choices = ["Océanos", "Bosques", "Desiertos", "Salir"],
                    IsMainMenu = true,
                }
            );

            switch (choice)
            {
                case -1:
                case 3:
                {
                    if (HandleExit(choice == 3))
                    {
                        loop = false;
                        break;
                    }
                    break;
                }
                case 0:
                {
                    var Animal = AnimalFactory.Create(HabitatType.Oceans);
                    Console.WriteLine(Animal.ToString());
                    Animal.PrintHabitat();
                    PressEnterToContinue();
                    break;
                }
                case 1:
                {
                    var Animal = AnimalFactory.Create(HabitatType.Forests);
                    Console.WriteLine(Animal.ToString());
                    Animal.PrintHabitat();
                    PressEnterToContinue();
                    break;
                }
                case 2:
                {
                    var Animal = AnimalFactory.Create(HabitatType.Deserts);
                    Console.WriteLine(Animal.ToString());
                    Animal.PrintHabitat();
                    PressEnterToContinue();
                    break;
                }
            }
        }
    }

    // Maneja la salida del programa, con confirmación si es necesario
    private bool HandleExit(bool shouldConfirm)
    {
        if (shouldConfirm)
        {
            var confirm = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Estas seguro que deseas salir?",
                    Choices = ["Si, deseo salir.", "No, no quiero salir ahora."],
                }
            );

            if (confirm == 0)
            {
                return true;
            }

            return false;
        }
        else
        {
            return true;
        }
    }
}
