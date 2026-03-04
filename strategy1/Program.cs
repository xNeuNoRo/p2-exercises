using App;

namespace EjercicioStragegy1;

public class Program
{
    protected Program() { }

    // Punto de entrada de la app
    public static void Main(string[] args)
    {
        var app = new StrategyApp();
        app.Run();
    }
}
