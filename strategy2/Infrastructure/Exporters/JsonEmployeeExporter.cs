using App.DTOs;
using App.Infrastructure.Interfaces;
using App.Infrastructure.Repositories.Json;

namespace App.Infrastructure.Exporters;

public class JsonEmployeeExporter : JsonBaseRepo<EmployeeExportDto>, IExporter<EmployeeExportDto>
{
    public JsonEmployeeExporter(string filePath)
        : base(filePath) { }

    public async Task Export(EmployeeExportDto employee)
    {
        // Guardamos el nuevo empleado en el archivo JSON
        await AppendAsync(employee);
        // Informamos que el empleado ha sido exportado exitosamente
        Console.WriteLine($"Empleado {employee.Nombre} exportado a JSON exitosamente.");
        Console.WriteLine("Presiona [Enter] para continuar...");
        Console.ReadLine();
    }
}
