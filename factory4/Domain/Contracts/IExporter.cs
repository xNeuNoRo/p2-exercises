using App.Domain.Entities;

namespace App.Domain.Contracts;

public interface IExporter
{
    void export(Student student);
}
