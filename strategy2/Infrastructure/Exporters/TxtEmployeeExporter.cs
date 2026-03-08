using App.Domain.Entities;
using App.DTOs;
using App.Infrastructure.Interfaces;
using App.Infrastructure.Repositories.Txt;

namespace App.Infrastructure.Exporters;

public class TxtEmployeeExporter : TxtBaseRepo<EmployeeExportDto>, IExporter<EmployeeExportDto>
{
    public TxtEmployeeExporter(string filePath)
        : base(filePath) { }

    public async Task Export(EmployeeExportDto employee)
    {
        // Guardamos el nuevo empleado en el archivo TXT
        Append(employee);
        // Simulamos una operación asíncrona
        await Task.CompletedTask;
        // Informamos que el empleado ha sido exportado exitosamente
        Console.WriteLine($"Empleado {employee.Nombre} exportado a TXT exitosamente.");
        Console.WriteLine("Presiona [Enter] para continuar...");
        Console.ReadLine();
    }
}
