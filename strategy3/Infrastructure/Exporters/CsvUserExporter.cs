using App.DTOs;
using App.Infrastructure.Interfaces;
using App.Infrastructure.Repositories.Csv;

namespace App.Infrastructure.Exporters;

public class CsvUserExporter : CsvBaseRepo<UserExportDto>, IExporter<UserExportDto>
{
    private readonly string _csvFileName;

    public CsvUserExporter(string filePath)
        : base(filePath)
    {
        _csvFileName = Path.GetFileName(filePath);
    }

    public async Task Export(UserExportDto user)
    {
        // Guardamos el nuevo usuario en el archivo CSV
        AppendItem(user);
        // Simulamos una operación asíncrona
        await Task.CompletedTask;
        // Informamos que el usuario ha sido exportado exitosamente
        Console.WriteLine(
            $"Usuario {user.Nombre} guardado como CSV en '{_csvFileName}' exitosamente."
        );
        Console.WriteLine("Presiona [Enter] para continuar...");
        Console.ReadLine();
    }
}
