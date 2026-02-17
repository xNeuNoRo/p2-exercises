using Adapters;
using Contracts;

class Program
{
    static void Main()
    {
        int option;
        while (true)
        {
            Console.WriteLine("=== Sistema de Relojes ===");
            Console.WriteLine("[1] - Reloj Digital");
            Console.WriteLine("[2] - Reloj Analogico");
            
            Console.Write("\nOpcion > ");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out option))
            {
                Console.WriteLine("Debes ingresar un numero valido!\n");
                continue;
            }

            if (option < 1 || option > 2)
            {
                Console.WriteLine("Debes ingresar una opcion valida!\n");
                continue;
            }

            break;
        }

        IReloj reloj = option switch
        {
            1 => new RelojDigital(),
            2 => new AdaptadorRelojAnalogico(),
            _ => throw new NotImplementedException("Tipo de reloj no implementado"),
        };

        Pantalla pantalla = new Pantalla();
        pantalla.MostrarHora(reloj);
    }
}
