using App;

namespace Ejercicio3Strategy;

public class Program
{
    protected Program() { }

    public static async Task Main(string[] args)
    {
        var app = new UserApp();
        await app.Run();
    }
}
