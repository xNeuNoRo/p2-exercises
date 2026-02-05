using App.Domain.Contracts;
using App.Domain.Entities;
using App.Infrastructure.Repositories;
using StringUtils = App.Helpers.String;

namespace App.Infrastructure.Exporters;

public class JsonExporter : IExporter
{
    private readonly JsonRepo<Student> _repo;

    public JsonExporter(string filePath)
    {
        string finalPath = StringUtils.EnsureExtension(filePath, "json");
        _repo = JsonRepo<Student>.GetInstance(finalPath);
    }

    public void export(Student student)
    {
        _repo.Append(student);
        Console.WriteLine($"Se ha exportado el estudiante {student.Name} a JSON correctamente.");
        Console.WriteLine("Presiona [Enter] para continuar...");
        Console.ReadLine();
    }
}
