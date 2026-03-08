using App.Domain.Entities;
using App.Domain.Enums;
using App.Extensions;
using App.Helpers;
using App.Infrastructure.Factories;
using App.Infrastructure.Services;

namespace App;

public class UserApp
{
    private List<User> _users { get; set; } = new List<User>();
    private readonly string[] _exporterChoices = ["JSON", "CSV", "TXT"];

    private void PressEnterToContinue()
    {
        Console.WriteLine("Presiona [Enter] para continuar...");
        Console.ReadLine();
    }

    public UserApp() { }

    public async Task Run()
    {
        bool loop = true;
        while (loop)
        {
            var choice = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Ejercicio 3 - Strategy\nDeveloped By Angel",
                    Choices =
                    [
                        "Crear Usuario",
                        "Ver Usuarios Creados (No guardados)",
                        "Guardar Usuarios",
                        "Salir del Programa",
                    ],
                }
            );

            switch (choice)
            {
                case -1:
                case 3:
                {
                    if (HandleExit(true))
                    {
                        loop = false;
                    }
                    break;
                }
                case 0:
                    HandleCreateUser();
                    break;
                case 1:
                    HandleViewUsers();
                    break;
                case 2:
                {
                    await HandleExportUsers(_users);
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

    // =====================================
    // Logica de Vistas
    // (Lo dejo aqui para no complicar el ejercicio)
    // =====================================

    private void HandleViewUsers()
    {
        Console.Clear();
        if (_users.Count == 0)
        {
            Console.WriteLine("No hay usuarios creados.\n\n");
        }
        else
        {
            Console.WriteLine("Usuarios Creados (No guardados):\n");
            foreach (var user in _users)
            {
                Console.WriteLine(user.ToString());
            }
            Console.WriteLine();
        }
        PressEnterToContinue();
    }

    private async Task HandleExportUsers(List<User> users)
    {
        if (users.Count == 0)
        {
            Console.WriteLine("No hay usuarios para guardar.\n\n");
            PressEnterToContinue();
            return;
        }

        var choice = InteractiveMenu.Show(
            new InteractiveMenu.MenuArgs
            {
                MenuTitle = "Selecciona el formato de guardado",
                Choices = _exporterChoices,
            }
        );

        string filePath = $"users.{_exporterChoices[choice].ToLower()}";
        var exporterType = choice switch
        {
            0 => ExporterType.Json,
            1 => ExporterType.Csv,
            2 => ExporterType.Txt,
            _ => throw new InvalidOperationException("Opción no válida"),
        };
        var exporter = new ExporterContext(ExporterFactory.CreateExporter(exporterType, filePath));
        foreach (var user in users.ToList())
        {
            await exporter.Export(user.ToExportDto());
        }
        users.Clear();
    }

    // =====================================
    // Logica de Servicios
    // (Lo dejo aqui para no complicar el ejercicio)
    // =====================================

    private void HandleCreateUser()
    {
        var user = CreateUser();
        _users.Add(user);
        Console.WriteLine("\nUsuario creado exitosamente!\n\n");
        PressEnterToContinue();
    }

    private User CreateUser()
    {
        Console.Clear();
        Input.ReadRequiredStrArgs strArgs = new Input.ReadRequiredStrArgs { AllowEmpty = false };
        Input.ReadRequiredIntArgs intArgs = new Input.ReadRequiredIntArgs { AllowEmpty = false };

        string name = Input.ReadRequiredStr("Ingresa el nombre del usuario: ", strArgs);

        int? age;
        while (true)
        {
            age = Input.ReadRequiredInt("Ingresa la edad del usuario: ", intArgs);
            if (!age.HasValue || age <= 0 || age > 120)
            {
                Console.WriteLine("Edad no válida. Por favor, ingresa una edad entre 1 y 120.\n");
            }
            else
            {
                break;
            }
        }

        string email = Input.ReadRequiredStr("Ingresa el correo del usuario: ", strArgs);

        return new User(name, age.Value, email);
    }
}
