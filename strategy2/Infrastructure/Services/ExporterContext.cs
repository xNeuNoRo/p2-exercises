using App.DTOs;
using App.Infrastructure.Interfaces;

namespace App.Infrastructure.Services;

public class ExporterContext
{
    private readonly IExporter<EmployeeExportDto> _exporter;

    public ExporterContext(IExporter<EmployeeExportDto> exporter)
    {
        _exporter = exporter;
    }

    public async Task Export(EmployeeExportDto employee)
    {
        await _exporter.Export(employee);
    }
}
