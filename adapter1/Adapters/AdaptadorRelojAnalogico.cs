using Contracts;

namespace Adapters;

public class AdaptadorRelojAnalogico : IReloj
{
    public string ObtenerHoraActual()
    {
        // Pedir al usuario que ingrese la hora, minutos y segundos
        // Pasamos un handler que valide que la hora este entre 0 y 23, los minutos entre 0 y 59 y los segundos entre 0 y 59
        int hora = ReadInt("Ingrese la hora (0-23): ", h => h >= 0 && h <= 23) ?? 0;
        int minutos = ReadInt("Ingrese los minutos (0-59): ", m => m >= 0 && m <= 59) ?? 0;
        int segundos = ReadInt("Ingrese los segundos (0-59): ", s => s >= 0 && s <= 59) ?? 0;

        // Crear una instancia de RelojAnalogico con la hora, minutos y segundos ingresados
        var reloj = new RelojAnalogico(hora, minutos, segundos);

        return $"{reloj.ObtenerHora():D2}:{reloj.ObtenerMinutos():D2}:{reloj.ObtenerSegundos():D2}";
    }

    private static int? ReadInt(string? prompt, Func<int, bool>? handler = null)
    {
        while (true)
        {
            // Imprimir un prompt inicial si se le pasa
            if (!string.IsNullOrEmpty(prompt))
            {
                Console.Write(prompt);
            }

            string? strValue = Console.ReadLine();

            // Validar si es vacio o nulo
            if (string.IsNullOrWhiteSpace(strValue))
            {
                Console.WriteLine("Debes ingresar un valor valido!");
                continue;
            }

            // Intentar parsear el entero
            if (int.TryParse(strValue, out int number))
            {
                // Si se le paso un handler, validar el numero con el handler
                if (handler is not null && !handler(number))
                {
                    Console.WriteLine("El numero ingresado no es valido, intenta de nuevo.");
                    continue;
                }

                // Si el numero es valido, retornarlo
                return number;
            }

            Console.WriteLine("Debes ingresar un numero entero valido.");
        }
    }
}
