using App.Domain.Contracts;
using App.Domain.Entities;
using App.Domain.Enums;
using App.Domain.Factories;
using App.Helpers;
using App.Infrastructure.Adapters;
using App.Infrastructure.Repositories;
using App.Services;
using StringUtils = App.Helpers.String;

namespace App;

public class AdapterApp
{
    private readonly string fileName = "students";
    private readonly StudentService _studentService;
    private readonly StudentFactory _studentFactory;

    private static void PressEnterToContinue()
    {
        Console.WriteLine("\nPresiona [Enter] para continuar...");
        Console.ReadLine();
    }

    public AdapterApp(FileType fileType)
    {
        _studentFactory = new StudentFactory();
        IFileRepo<Student> repo = fileType switch
        {
            FileType.Json => JsonRepo<Student>.GetInstance(
                StringUtils.EnsureExtension(fileName, "json")
            ),
            FileType.Csv => new CsvRepoAdapter<Student>(
                StringUtils.EnsureExtension(fileName, "csv")
            ),
            _ => throw new ArgumentOutOfRangeException(
                nameof(fileType),
                fileType,
                "Tipo de archivo no soportado"
            ),
        };
        _studentService = new StudentService(repo);
    }

    public void Run()
    {
        // Inyectamos un nuevo estudiante usando el servicio,
        // que a su vez delega al repositorio (que es el adaptador)
        // para guardar el estudiante en el archivo CSV
        _studentService.AddStudent(
            _studentFactory.Create(
                "20251122",
                "Angel Gonzalez M.",
                "20251122@itla.edu.do",
                "Desarrollo de Software"
            )
        );

        bool loop = true;
        while (loop)
        {
            int choice = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Adapter Pattern App -  Menu Principal",
                    Choices = ["Listar estudiantes", "Agregar estudiante", "Salir"],
                }
            );

            switch (choice)
            {
                case -1:
                case 2:
                {
                    if (HandleExit(choice == 2))
                    {
                        loop = false;
                    }
                    break;
                }
                case 0:
                {
                    _studentService.GetAllStudents().ForEach(s => Console.WriteLine(s.ToString()));
                    PressEnterToContinue();
                    break;
                }
                case 1:
                {
                    var newStudent = CreateStudent();
                    _studentService.AddStudent(newStudent);
                    Console.WriteLine("\nEstudiante agregado exitosamente!");
                    PressEnterToContinue();
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

    private Student CreateStudent()
    {
        Input.ReadRequiredStrArgs strArgs = new Input.ReadRequiredStrArgs { AllowEmpty = false };

        Console.Clear();
        Console.WriteLine("=== Ingrese los datos del estudiante ===\n");

        string id = Input.ReadRequiredStr("Matricula: ", strArgs);
        string name = Input.ReadRequiredStr("Nombre: ", strArgs);
        string email = Input.ReadRequiredStr("Email: ", strArgs);
        string career = Input.ReadRequiredStr("Carrera: ", strArgs);

        return _studentFactory.Create(id, name, email, career);
    }
}
