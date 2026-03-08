using App.Domain.Entities;
using App.Domain.Enums;
using App.Infrastructure.Exporters;
using App.Infrastructure.Interfaces;

namespace App.Infrastructure.Factories;

public static class ExporterFactory
{
    public static IExporter<Employee> CreateExporter(ExporterType type, string filePath)
    {
        return type switch
        {
            ExporterType.Json => new JsonEmployeeExporter(filePath),
            ExporterType.Csv => new CsvEmployeeExporter(filePath),
            ExporterType.Txt => new TxtEmployeeExporter(filePath),
            _ => throw new NotImplementedException($"El exportador '{type}' no está implementado."),
        };
    }
}
