using App.Domain.Enums;
using App.Domain.Factories;
using App.Helpers;

namespace App;

public class ConverterApp
{
    private readonly CurrencyConverterFactory _currencyConverterFactory;

    private static void PressEnterToContinue()
    {
        Console.WriteLine("\nPresiona [Enter] para continuar...");
        Console.ReadLine();
    }

    public ConverterApp()
    {
        _currencyConverterFactory = new CurrencyConverterFactory();
    }

    public void Run()
    {
        bool loop = true;

        while (loop)
        {
            int selectedChoice = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Conversor de Monedas - Escoge una opción:",
                    Choices =
                    [
                        "Convertir de RD a Dólares (USD)",
                        "Convertir de RD a Euros (EUR)",
                        "Convertir de RD a Yenes (JPY)",
                        "Salir",
                    ],
                }
            );

            CurrencyType currency = CurrencyType.Dollar;

            switch (selectedChoice)
            {
                case -1:
                case 3:
                    if (HandleExit(selectedChoice == 2))
                    {
                        loop = false;
                    }
                    break;
                case 0:
                    currency = CurrencyType.Dollar;
                    break;
                case 1:
                    currency = CurrencyType.Euro;
                    break;
                case 2:
                    currency = CurrencyType.Yen;
                    break;
                default:
                    return;
            }

            decimal amount = AskForAmount();
            var converter = _currencyConverterFactory.Create(currency);
            decimal convertedAmount = converter.Convert(amount);
            Console.WriteLine($"Monto convertido: {convertedAmount:N2} {currency}");
            PressEnterToContinue();
        }
    }

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

    private decimal AskForAmount()
    {
        decimal? amount = Input.ReadRequiredDec(
            "Ingresa la cantidad en RD$: ",
            new Input.ReadRequiredDecArgs { AllowEmpty = false }
        );

        if (!amount.HasValue)
        {
            throw new InvalidOperationException("El monto no puede ser nulo.");
        }

        return amount.Value;
    }
}
