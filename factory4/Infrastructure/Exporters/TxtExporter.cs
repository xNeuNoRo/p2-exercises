using App.Domain.Contracts;
using App.Domain.Entities;
using App.Infrastructure.Repositories;
using StringUtils = App.Helpers.String;

namespace App.Infrastructure.Exporters;

public class TxtExporter : IExporter
{
    private readonly TxtRepo<Student> _repo;

    public TxtExporter(string filePath)
    {
        string finalPath = StringUtils.EnsureExtension(filePath, "txt");
        _repo = TxtRepo<Student>.GetInstance(finalPath);
    }

    public void export(Student student)
    {
        _repo.Append(student);
        Console.WriteLine($"Se ha exportado el estudiante {student.Name} a TXT correctamente.");
        Console.WriteLine("Presiona [Enter] para continuar...");
        Console.ReadLine();
    }
}
