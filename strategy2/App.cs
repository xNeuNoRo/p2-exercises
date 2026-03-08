using App.Domain.Entities;
using App.Domain.Enums;
using App.Domain.Factories;
using App.Domain.Services;
using App.Extensions;
using App.Helpers;
using App.Infrastructure.Factories;
using App.Infrastructure.Services;

namespace App;

public class EmployeeApp
{
    private List<Employee> _employees { get; set; } = new List<Employee>();
    private readonly string[] _exporterChoices = ["JSON", "CSV", "TXT"];

    private void PressEnterToContinue()
    {
        Console.WriteLine("Presiona [Enter] para continuar...");
        Console.ReadLine();
    }

    private int GetNextId()
    {
        if (_employees.Count == 0)
            return 1;

        // Si hay empleados, buscamos el ID mas alto y le sumamos 1
        return _employees.Max(i => i.Id) + 1;
    }

    public EmployeeApp() { }

    public async Task Run()
    {
        bool loop = true;
        while (loop)
        {
            var choice = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Ejercicio 2 - Strategy\nDeveloped By Angel",
                    Choices =
                    [
                        "Crear Empleado",
                        "Ver Empleados Creados (No exportados)",
                        "Exportar Empleados",
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
                    HandleCreateEmployee();
                    break;
                case 1:
                    HandleViewEmployees();
                    break;
                case 2:
                {
                    await HandleExportEmployees(_employees);
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

    private void HandleViewEmployees()
    {
        Console.Clear();
        if (_employees.Count == 0)
        {
            Console.WriteLine("No hay empleados creados.\n\n");
        }
        else
        {
            Console.WriteLine("Empleados Creados (No exportados):\n");
            foreach (var employee in _employees)
            {
                Console.WriteLine(employee.ToString());
            }
            Console.WriteLine();
        }
        PressEnterToContinue();
    }

    private async Task HandleExportEmployees(List<Employee> employees)
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("No hay empleados para exportar.\n\n");
            PressEnterToContinue();
            return;
        }

        var choice = InteractiveMenu.Show(
            new InteractiveMenu.MenuArgs
            {
                MenuTitle = "Selecciona el formato de exportación",
                Choices = _exporterChoices,
            }
        );

        string filePath = $"employees.{_exporterChoices[choice].ToLower()}";
        var exporterType = choice switch
        {
            0 => ExporterType.Json,
            1 => ExporterType.Csv,
            2 => ExporterType.Txt,
            _ => throw new InvalidOperationException("Opción no válida"),
        };
        var exporter = new ExporterContext(ExporterFactory.CreateExporter(exporterType, filePath));
        foreach (var employee in employees.ToList())
        {
            await exporter.Export(employee.ToExportDto());
        }
        employees.Clear();
    }

    // =====================================
    // Logica de Servicios
    // (Lo dejo aqui para no complicar el ejercicio)
    // =====================================

    private void HandleCreateEmployee()
    {
        var employee = CreateEmployee();
        _employees.Add(employee);
        Console.WriteLine("\nEmpleado creado exitosamente!\n\n");
        PressEnterToContinue();
    }

    private Employee CreateEmployee()
    {
        Console.Clear();
        Input.ReadRequiredStrArgs strArgs = new Input.ReadRequiredStrArgs { AllowEmpty = false };
        Input.ReadRequiredDecArgs decArgs = new Input.ReadRequiredDecArgs { AllowEmpty = false };

        int id = GetNextId();
        string name = Input.ReadRequiredStr("Ingresa el nombre del empleado: ", strArgs);
        EmployeeType type = AskForEmployeeType();
        Console.Clear();
        string department = Input.ReadRequiredStr(
            "Ingresa el departamento del empleado: ",
            strArgs
        );
        decimal? salary;

        while (true)
        {
            salary = Input.ReadRequiredDec("Ingresa el salario del empleado: ", decArgs);

            if (!salary.HasValue)
            {
                Console.WriteLine("El salario es requerido.\n\n");
            }
            else
            {
                break;
            }
        }

        var tax = new TaxCalculator(TaxFactory.GetTaxStrategy(type)).CalculateTax(salary.Value);

        return new Employee(id, name, type, department, salary.Value, tax);
    }

    private EmployeeType AskForEmployeeType()
    {
        var choice = InteractiveMenu.Show(
            new InteractiveMenu.MenuArgs
            {
                MenuTitle = "Selecciona el tipo de empleado",
                Choices = ["Tiempo Completo", "Medio Tiempo", "Contratista"],
            }
        );

        var employeeType = choice switch
        {
            0 => EmployeeType.FullTime,
            1 => EmployeeType.PartTime,
            2 => EmployeeType.Contractor,
            _ => throw new InvalidOperationException("Opción no válida"),
        };

        return employeeType;
    }
}
