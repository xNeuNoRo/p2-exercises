using App.Domain;

namespace EjercicioSingleton3;

public static class Program
{
    public static void Main(string[] args)
    {
        var codeGen = Code.GetInstance(); // obtener una vez

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"\nCodigo generado: {codeGen.Count()}");
            Console.Write("[Enter] = siguiente codigo | 'reset' = reiniciar el contador | 'salir' = salir del programa: ");

            string? input = Console.ReadLine()?.Trim().ToLower();

            if (string.IsNullOrEmpty(input))
            {
                continue;
            }

            if (input == "reset")
            {
                codeGen.Reset();
                Console.WriteLine("Contador reiniciado.");
                continue;
            }

            if (input == "salir")
            {
                break;
            }

            Console.WriteLine("Opción no válida.");
        }
    }
}
