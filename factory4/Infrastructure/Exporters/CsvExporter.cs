using App.Domain.Contracts;
using App.Domain.Entities;
using App.Infrastructure.Repositories;
using StringUtils = App.Helpers.String;

namespace App.Infrastructure.Exporters;

public class CsvExporter : IExporter
{
    private readonly CsvRepo<Student> _repo;

    public CsvExporter(string filePath)
    {
        string finalPath = StringUtils.EnsureExtension(filePath, "csv");
        _repo = CsvRepo<Student>.GetInstance(finalPath);
    }

    public void export(Student student)
    {
        _repo.Append(student);
        Console.WriteLine($"Se ha exportado el estudiante {student.Name} a CSV correctamente.");
        Console.WriteLine("Presiona [Enter] para continuar...");
        Console.ReadLine();
    }
}
