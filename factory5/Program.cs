using App;

namespace EjercicioFactory5;

public class Program
{
    protected Program() { }

    public static void Main(string[] args)
    {
        // Iniciar la aplicacion
        var app = new EncryptorApp();
        // Correr la aplicacion
        app.Run();
    }
}
