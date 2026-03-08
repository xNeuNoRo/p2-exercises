using App.Domain.Entities;
using App.Infrastructure.Interfaces;
using App.Infrastructure.Repositories.Csv;

namespace App.Infrastructure.Exporters;

public class CsvEmployeeExporter : CsvBaseRepo<Employee>, IExporter<Employee>
{
    public CsvEmployeeExporter(string filePath)
        : base(filePath) { }

    public async Task Export(Employee employee)
    {
        // Guardamos el nuevo empleado en el archivo CSV
        AppendItem(employee);
        // Simulamos una operación asíncrona
        await Task.CompletedTask;
        // Informamos que el empleado ha sido exportado exitosamente
        Console.WriteLine($"Empleado {employee.Name} exportado a CSV exitosamente.");
        Console.WriteLine("Presiona [Enter] para continuar...");
        Console.ReadLine();
    }
}
