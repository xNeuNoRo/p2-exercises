using App.Domain.Entities;
using App.Domain.Enums;
using App.Domain.Factories;
using App.Domain.Services;
using App.Helpers;
using App.Infrastructure.Repositories;
using App.Services;

namespace App;

public class EmployeeApp
{
    private readonly EmployeeService _service;

    public EmployeeApp()
    {
        EmployeeRepository repo = new EmployeeRepository("employees.json");
        _service = new EmployeeService(repo);
    }

    private void PressEnterToContinue()
    {
        Console.WriteLine("Presiona [Enter] para continuar...");
        Console.ReadLine();
    }

    public async Task Run()
    {
        bool loop = true;
        while (loop)
        {
            var choice = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Practica - Strategy\nDeveloped By Angel",
                    Choices = ["Crear Empleado", "Ver Empleados", "Salir del Programa"],
                }
            );

            switch (choice)
            {
                case -1:
                case 2:
                {
                    if (HandleExit(true))
                    {
                        loop = false;
                    }
                    break;
                }
                case 0:
                    await HandleCreateEmployee();
                    break;
                case 1:
                    await HandleViewEmployees();
                    break;
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

    private async Task HandleViewEmployees()
    {
        var employees = await _service.GetAllEmployees();
        Console.Clear();
        if (!employees.Any())
        {
            Console.WriteLine("No hay empleados creados.\n\n");
        }
        else
        {
            Console.WriteLine("Empleados Creados (No exportados):\n");
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.ToString());
            }
            Console.WriteLine();
        }
        PressEnterToContinue();
    }

    private async Task HandleCreateEmployee()
    {
        var employee = CreateEmployee();
        await _service.AddEmployee(employee);
        Console.WriteLine("\nEmpleado creado exitosamente!\n\n");
        PressEnterToContinue();
    }

    private Employee CreateEmployee()
    {
        Console.Clear();
        Input.ReadRequiredStrArgs strArgs = new Input.ReadRequiredStrArgs { AllowEmpty = false };

        string name = Input.ReadRequiredStr("Ingresa el nombre del empleado: ", strArgs);
        EmployeeType type = AskForEmployeeType();
        Console.Clear();
        string department = Input.ReadRequiredStr(
            "Ingresa el departamento del empleado: ",
            strArgs
        );
        var salary = new SalaryCalculator(SalaryFactory.GetSalaryStrategy(type)).CalculateSalary();

        return new Employee(name, type, department, salary);
    }

    private EmployeeType AskForEmployeeType()
    {
        var choice = InteractiveMenu.Show(
            new InteractiveMenu.MenuArgs
            {
                MenuTitle = "Selecciona el tipo de empleado",
                Choices = ["Empleado Asalariado", "Empleado por Hora", "Empleado por Comisión"],
            }
        );

        var employeeType = choice switch
        {
            0 => EmployeeType.Salaried,
            1 => EmployeeType.PerHour,
            2 => EmployeeType.Commission,
            _ => throw new InvalidOperationException("Opción no válida"),
        };

        return employeeType;
    }
}
