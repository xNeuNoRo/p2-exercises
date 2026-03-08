using App.DTOs;
using App.Infrastructure.Interfaces;

namespace App.Infrastructure.Services;

public class ExporterContext
{
    private readonly IExporter<UserExportDto> _exporter;

    public ExporterContext(IExporter<UserExportDto> exporter)
    {
        _exporter = exporter;
    }

    public async Task Export(UserExportDto user)
    {
        await _exporter.Export(user);
    }
}
