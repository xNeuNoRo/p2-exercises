using App.Helpers;
using Calculator.Business.Factories;
using Calculator.Business.Services;
using Calculator.Data.Repositories;
using Calculator.Entities.Models;
using Calculator.Presentation.Views;

namespace Calculator.Presentation;

public class CalculatorApp
{
    private readonly CalculatorService _service;
    private readonly OperationView _operationView;

    public CalculatorApp()
    {
        TxtRepo<Operation> repo = TxtRepo<Operation>.GetInstance("history.txt");
        OperationFactory factory = new OperationFactory();
        _service = new CalculatorService(repo, factory);
        _operationView = new OperationView(_service);
    }

    public void Run()
    {
        bool loop = true;
        while (loop)
        {
            int choice = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Calculadora N-Capas\nDeveloped by Angel Gonzalez (2025-1122)",
                    Choices = ["Realizar una operacion", "Ver historial", "Salir"],
                }
            );

            switch (choice)
            {
                case -1:
                case 2:
                    if (HandleExit(choice == 2))
                    {
                        loop = false;
                    }
                    break;
                case 0:
                {
                    _operationView.Show();
                    break;
                }
                case 1:
                {
                    HandleViewHistory();
                    break;
                }
            }
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

    private void HandleViewHistory()
    {
        var history = _service.GetHistory();
        Console.Clear();
        Console.WriteLine("Historial de Operaciones:");
        Console.WriteLine("-------------------------");

        if (history.Count == 0)
        {
            Console.WriteLine("No se han realizado operaciones aun.");
            Input.PressEnterToContinue();
            return;
        }

        for (int i = 0; i < history.Count; i++)
        {
            var operation = history[i];
            Console.WriteLine($"=== Operacion #{i + 1} ===");
            Console.WriteLine(OperationDisplay.FormatToText(operation));
            Console.WriteLine("-------------------------");
        }
        Input.PressEnterToContinue();
    }
}
