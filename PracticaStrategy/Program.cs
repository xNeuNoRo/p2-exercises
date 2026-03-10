using App;

namespace PracticaStrategy;

public class Program
{
    protected Program() { }

    public static async Task Main(string[] args)
    {
        var app = new EmployeeApp();
        await app.Run();
    }
}
