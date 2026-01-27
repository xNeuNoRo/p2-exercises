using App.Helpers;

namespace App;

public class TemperatureApp
{
    private const string _filePath = "temperatures.txt";

    private static void PressEnterToContinue()
    {
        Console.WriteLine("\nPresiona [Enter] para continuar...");
        Console.ReadLine();
    }

    public TemperatureApp()
    {
        // Aseguramos que el archivo de texto tenga el header correcto
        Txt.EnsureHeader(_filePath, "Fecha\t\t\tTemperatura");
    }

    public void Run()
    {
        // Bucle principal de la app
        bool loop = true;
        while (loop)
        {
            int option = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Programa de Temperaturas",
                    Choices =
                    [
                        "Registrar temperatura",
                        "Mostrar temperaturas registradas",
                        "Salir",
                    ],
                    IsMainMenu = true,
                }
            );

            switch (option)
            {
                case -1:
                case 2:
                {
                    if (HandleExit(option == 2))
                    {
                        loop = false;
                        break;
                    }
                    break;
                }
                case 0:
                {
                    HandleRegisterTemperature();
                    break;
                }
                case 1:
                {
                    Txt.PrintTxt(_filePath);
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

    private void HandleRegisterTemperature()
    {
        Console.Clear();
        Console.WriteLine("=== Registrar nueva temperatura ===\n");

        double? temperature = Input.ReadRequiredDouble(
            "Ingresa la temperatura (°C): ",
            new Input.ReadRequiredDoubleArgs { AllowEmpty = false }
        );
        DateTime dateTime = Input.ReadRequiredDateTime(
            "Ingresa la fecha (dd/MM/yyyy): ",
            new Input.ReadRequiredDateTimeArgs { Format = "dd/MM/yyyy", Culture = "es-ES" }
        );

        string line = $"{dateTime:dd/MM/yyyy}\t-\t{temperature:0.0}°C";
        Txt.AppendLine(_filePath, line);

        Console.WriteLine("\nTemperatura registrada exitosamente!");
        PressEnterToContinue();
    }
}
