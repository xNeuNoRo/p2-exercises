class Program
{
    static void Main(string[] args)
    {
        int option;
        while (true)
        {
            Console.WriteLine("=== Sistema de fotocopiadoras ===");
            Console.WriteLine("[1] - Fotocopiadora Canon");
            Console.WriteLine("[2] - Fotocopiadora Epson");
            Console.WriteLine("[3] - Fotocopiadora Industrial");

            Console.Write("\nOpcion > ");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out option))
            {
                Console.WriteLine("Debes ingresar un numero valido!\n");
                continue;
            }

            if (option < 1 || option > 3)
            {
                Console.WriteLine("Debes ingresar una opcion valida!\n");
                continue;
            }

            break;
        }

        IFotoCopiadora fotoCopiadora = option switch
        {
            1 => new FotocopiadoraCanon(),
            2 => new FotocopiadoraEpsonAdapter(),
            3 => new FotocopiadoraIndustrialAdapter(),
            _ => throw new InvalidOperationException("Tipo de fotocopiadora no valido."),
        };

        Aula aula = new Aula(fotoCopiadora);
        aula.ReproducirMaterial("Implementando patrón de diseño Adapter", 3);
    }
}
