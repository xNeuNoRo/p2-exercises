using App;

namespace Practica2Factory;

public class Program
{
    protected Program() { }

    public static void Main(string[] args)
    {
        var app = new RolesApp();
        app.Run();
    }
}
