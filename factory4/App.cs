using App.Domain.Entities;
using App.Domain.Enums;
using App.Helpers;
using App.Infrastructure.Factories;

namespace App;

public class ExporterApp
{
    private readonly ExporterFactory _factory;
    private readonly string filePath = "students_data";
    private readonly ExporterType[] _exporterTypes =
    [
        ExporterType.JSON,
        ExporterType.CSV,
        ExporterType.TXT,
    ];

    public ExporterApp()
    {
        _factory = new ExporterFactory();
    }

    public void Run()
    {
        bool loop = true;
        while (loop)
        {
            int selectedChoice = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Selecciona el formato para exportar el estudiante:",
                    Choices = ["JSON", "CSV", "TXT", "Salir"],
                }
            );

            switch (selectedChoice)
            {
                case -1:
                case 3:
                {
                    if (HandleExit(selectedChoice == 3))
                    {
                        loop = false;
                    }
                    break;
                }
                case 0:
                case 1:
                case 2:
                {
                    // Solicitar datos del estudiante
                    Student student = CreateStudent();
                    // Crear el exportador y exportar el estudiante
                    var exporter = _factory.Create(_exporterTypes[selectedChoice], filePath);
                    // Exportar el estudiante
                    exporter.export(student);
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

    // Solicita los datos de un estudiante al usuario y retorna el objeto Student creado
    private Student CreateStudent()
    {
        Input.ReadRequiredStrArgs strArgs = new Input.ReadRequiredStrArgs { AllowEmpty = false };

        Console.Clear();
        Console.WriteLine("=== Ingrese los datos del estudiante ===\n");

        string name = Input.ReadRequiredStr("Nombre: ", strArgs);
        string id = Input.ReadRequiredStr("Matricula: ", strArgs);
        string career = Input.ReadRequiredStr("Carrera: ", strArgs);

        return new Student(id, name, career);
    }
}
