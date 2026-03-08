using App.DTOs;
using App.Infrastructure.Interfaces;
using App.Infrastructure.Repositories.Csv;

namespace App.Infrastructure.Exporters;

public class CsvEmployeeExporter : CsvBaseRepo<EmployeeExportDto>, IExporter<EmployeeExportDto>
{
    public CsvEmployeeExporter(string filePath)
        : base(filePath) { }

    public async Task Export(EmployeeExportDto employee)
    {
        // Guardamos el nuevo empleado en el archivo CSV
        AppendItem(employee);
        // Simulamos una operación asíncrona
        await Task.CompletedTask;
        // Informamos que el empleado ha sido exportado exitosamente
        Console.WriteLine($"Empleado {employee.Nombre} exportado a CSV exitosamente.");
        Console.WriteLine("Presiona [Enter] para continuar...");
        Console.ReadLine();
    }
}
