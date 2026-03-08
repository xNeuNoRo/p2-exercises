using App.DTOs;
using App.Domain.Enums;
using App.Infrastructure.Exporters;
using App.Infrastructure.Interfaces;

namespace App.Infrastructure.Factories;

public static class ExporterFactory
{
    public static IExporter<UserExportDto> CreateExporter(ExporterType type, string filePath)
    {
        return type switch
        {
            ExporterType.Json => new JsonUserExporter(filePath),
            ExporterType.Csv => new CsvUserExporter(filePath),
            ExporterType.Txt => new TxtUserExporter(filePath),
            _ => throw new NotImplementedException($"El exportador '{type}' no está implementado."),
        };
    }
}
