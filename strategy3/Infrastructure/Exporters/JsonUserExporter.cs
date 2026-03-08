using App.DTOs;
using App.Infrastructure.Interfaces;
using App.Infrastructure.Repositories.Json;

namespace App.Infrastructure.Exporters;

public class JsonUserExporter : JsonBaseRepo<UserExportDto>, IExporter<UserExportDto>
{
    private readonly string _jsonFileName;

    public JsonUserExporter(string filePath)
        : base(filePath)
    {
        _jsonFileName = Path.GetFileName(filePath);
    }

    public async Task Export(UserExportDto user)
    {
        // Guardamos el nuevo usuario en el archivo JSON
        await AppendAsync(user);
        // Informamos que el usuario ha sido exportado exitosamente
        Console.WriteLine(
            $"Usuario {user.Nombre} guardado como JSON en '{_jsonFileName}' exitosamente."
        );
        Console.WriteLine("Presiona [Enter] para continuar...");
        Console.ReadLine();
    }
}
