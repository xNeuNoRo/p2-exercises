using App.Helpers;
using Calculator.Business.Services;
using Calculator.Entities.Enums;
using Calculator.Entities.Models;

namespace Calculator.Presentation.Views;

public class OperationView
{
    private readonly CalculatorService _service;

    public OperationView(CalculatorService service) => _service = service;

    public void Show()
    {
        bool backToMainMenu = false;
        while (!backToMainMenu)
        {
            int choice = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Escoge el tipo de operacion",
                    Choices =
                    [
                        "Suma (+)",
                        "Resta (-)",
                        "Multiplicacion (*)",
                        "Division (/)",
                        "Volver al menu principal",
                    ],
                }
            );

            if (choice == -1 || choice == 4)
            {
                backToMainMenu = true;
                continue; // Salir del menu de operaciones y volver al menu principal
            }

            OperationType type = choice switch
            {
                0 => OperationType.Addition,
                1 => OperationType.Subtraction,
                2 => OperationType.Multiplication,
                3 => OperationType.Division,
                _ => throw new InvalidOperationException("Opcion no valida."),
            };

            // Realizar la operacion y mostrar el resultado
            try
            {
                HandleOperation(type);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ha ocurrido un error: {ex.Message}");
            }
        }
    }

    // Este metodo se encarga de manejar la logica de realizar una operacion segun el tipo seleccionado por el usuario
    private void HandleOperation(OperationType type)
    {
        try
        {
            var (A, B) = AskForNumbers();
            var res = _service.Calculate(A, B, type);
            Console.WriteLine(OperationDisplay.FormatToText(res));
            Console.WriteLine("Se ha guardado la operacion en el historial.\n");
            Console.WriteLine("Presiona [Enter] para continuar...");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("\nPresiona [Enter] para continuar...");
            Console.ReadLine();
        }
    }

    // Este metodo se encarga de pedir al usuario que ingrese los numeros A y B para realizar la operacion
    private (decimal A, decimal B) AskForNumbers()
    {
        Input.ReadRequiredDecArgs decimalArgs = new Input.ReadRequiredDecArgs
        {
            AllowEmpty = false,
        };

        decimal? numberA = Input.ReadRequiredDec("Ingresa el primer numero (A): ", decimalArgs);
        decimal? numberB = Input.ReadRequiredDec("Ingresa el primer numero (B): ", decimalArgs);

        if (!numberA.HasValue || !numberB.HasValue)
            throw new InvalidOperationException(
                "Hubo un error al leer los numeros. Por favor, intenta de nuevo."
            );

        return (numberA.Value, numberB.Value);
    }
}
