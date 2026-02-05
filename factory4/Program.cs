using App;

namespace EjercicioFactory4;

public class Program
{
    protected Program() { }

    public static void Main(string[] args)
    {
        var app = new ExporterApp();
        app.Run();
    }
}
