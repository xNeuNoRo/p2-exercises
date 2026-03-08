using App.Domain.Entities;
using App.Infrastructure.Interfaces;

namespace App.Infrastructure.Services;

public class ExporterContext
{
    private readonly IExporter<Employee> _exporter;

    public ExporterContext(IExporter<Employee> exporter)
    {
        _exporter = exporter;
    }

    public async Task Export(Employee employee)
    {
        await _exporter.Export(employee);
    }
}
