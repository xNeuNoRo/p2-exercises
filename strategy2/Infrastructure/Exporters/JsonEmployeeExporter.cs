using App.Domain.Entities;
using App.Infrastructure.Interfaces;
using App.Infrastructure.Repositories.Json;

namespace App.Infrastructure.Exporters;

public class JsonEmployeeExporter : JsonBaseRepo<Employee>, IExporter<Employee>
{
    public JsonEmployeeExporter(string filePath)
        : base(filePath) { }

    public async Task Export(Employee employee)
    {
        // Guardamos el nuevo empleado en el archivo JSON
        await AppendAsync(employee);
        // Informamos que el empleado ha sido exportado exitosamente
        Console.WriteLine($"Empleado {employee.Name} exportado a JSON exitosamente.");
        Console.WriteLine("Presiona [Enter] para continuar...");
        Console.ReadLine();
    }
}
