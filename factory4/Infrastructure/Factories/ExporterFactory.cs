using App.Domain.Contracts;
using App.Domain.Enums;
using App.Infrastructure.Exporters;

namespace App.Infrastructure.Factories;

public class ExporterFactory
{
    public IExporter Create(ExporterType type, string filePath)
    {
        return type switch
        {
            ExporterType.JSON => new JsonExporter(filePath),
            ExporterType.CSV => new CsvExporter(filePath),
            ExporterType.TXT => new TxtExporter(filePath),
            _ => throw new ArgumentException("Tipo de exportador no soportado"),
        };
    }
}
