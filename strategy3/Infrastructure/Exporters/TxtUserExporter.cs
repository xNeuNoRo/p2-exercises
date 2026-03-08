using App.DTOs;
using App.Infrastructure.Interfaces;
using App.Infrastructure.Repositories.Txt;

namespace App.Infrastructure.Exporters;

public class TxtUserExporter : TxtBaseRepo<UserExportDto>, IExporter<UserExportDto>
{
    private readonly string _txtFileName;

    public TxtUserExporter(string filePath)
        : base(filePath)
    {
        _txtFileName = Path.GetFileName(filePath);
    }

    public async Task Export(UserExportDto user)
    {
        // Guardamos el nuevo usuario en el archivo TXT
        Append(user);
        // Simulamos una operación asíncrona
        await Task.CompletedTask;
        // Informamos que el usuario ha sido exportado exitosamente
        Console.WriteLine(
            $"Usuario {user.Nombre} guardado como TXT en '{_txtFileName}' exitosamente."
        );
        Console.WriteLine("Presiona [Enter] para continuar...");
        Console.ReadLine();
    }
}
