using App;
using App.Domain.Enums;
using App.Helpers;

namespace EjercicioAdapter2;

public class Program
{
    public static void Main(string[] args)
    {
        var fileType = AskFileType();
        var app = new AdapterApp(fileType);
        app.Run();
    }

    private static FileType AskFileType()
    {
        int choice = InteractiveMenu.Show(
            new InteractiveMenu.MenuArgs
            {
                MenuTitle = "Seleccione el tipo de archivo a usar:",
                Choices = ["Archivo JSON", "Archivo CSV"],
            }
        );

        return choice switch
        {
            0 => FileType.Json,
            1 => FileType.Csv,
            _ => throw new InvalidOperationException("Opcion no valida"),
        };
    }
}
