using System.Globalization;

namespace App.Helpers;

public static class Input
{
    private static readonly string _invalidInputMessage = "Debes ingresar un valor valido!";

    public class ReadRequiredStrArgs
    {
        public bool AllowEmpty { get; set; } = false;
    }

    // Pedir un input string valido
    public static string ReadRequiredStr(string? prompt, ReadRequiredStrArgs args)
    {
        string? strValue;

        do
        {
            // Imprimir un prompt inicial si se le pasa
            if (!string.IsNullOrEmpty(prompt))
            {
                Console.Write(prompt);
            }

            // Solicitar el string
            strValue = Console.ReadLine();

            // Validar si es vacio o nulo dependiendo del allowEmpty
            if (!args.AllowEmpty && string.IsNullOrWhiteSpace(strValue))
            {
                Console.WriteLine(_invalidInputMessage);
            }
            else
            {
                // Rompemos el bucle si es valido
                break;
            }
        } while (true);

        // Removemos cualquier posible espacio en blanco con el trim()
        return (strValue ?? string.Empty).Trim();
    }

    public class ReadRequiredIntArgs
    {
        public bool AllowEmpty { get; set; } = false;
        public double? MinValue { get; set; } = null;
    }

    // Pedir un input entero valido
    public static int? ReadRequiredInt(string? prompt, ReadRequiredIntArgs args)
    {
        while (true)
        {
            // Imprimir un prompt inicial si se le pasa
            if (!string.IsNullOrEmpty(prompt))
            {
                Console.Write(prompt);
            }

            string? strValue = Console.ReadLine();

            // Validar si es vacio o nulo dependiendo del allowEmpty
            if (string.IsNullOrWhiteSpace(strValue))
            {
                if (args.AllowEmpty)
                {
                    // Retorna null si se permite vacio
                    return null;
                }

                Console.WriteLine(_invalidInputMessage);
                continue;
            }

            // Intentar parsear el entero
            if (int.TryParse(strValue, out int number) && number >= args.MinValue)
            {
                return number;
            }

            Console.WriteLine("Debes ingresar un numero entero valido.");
        }
    }

    public class ReadRequiredDoubleArgs
    {
        public bool AllowEmpty { get; set; } = false;
        public double? MinValue { get; set; } = null;
    }

    public static double? ReadRequiredDouble(string? prompt, ReadRequiredDoubleArgs args)
    {
        while (true)
        {
            // Imprimir un prompt inicial si se le pasa
            if (!string.IsNullOrEmpty(prompt))
                Console.Write(prompt);

            string? strValue = Console.ReadLine();

            // Validar si es vacio o nulo dependiendo del allowEmpty
            if (string.IsNullOrWhiteSpace(strValue))
            {
                if (args.AllowEmpty)
                {
                    // Retorna null si se permite vacio
                    return null;
                }

                Console.WriteLine(_invalidInputMessage);
                continue;
            }

            // Intentar parsear el double
            if (!double.TryParse(strValue, out double number))
            {
                Console.WriteLine("Debes ingresar un numero valido.");
                continue;
            }

            if (args.MinValue.HasValue && number < args.MinValue.Value)
            {
                Console.WriteLine($"El numero debe ser mayor o igual a {args.MinValue.Value}.");
                continue;
            }

            return number;
        }
    }

    public class ReadRequiredDecArgs
    {
        public bool AllowEmpty { get; set; } = false;
    }

    public static decimal? ReadRequiredDec(string? prompt, ReadRequiredDecArgs args)
    {
        while (true)
        {
            // Imprimir un prompt inicial si se le pasa
            if (!string.IsNullOrEmpty(prompt))
                Console.Write(prompt);

            string? strValue = Console.ReadLine();

            // Validar si es vacio o nulo dependiendo del allowEmpty
            if (string.IsNullOrWhiteSpace(strValue))
            {
                if (args.AllowEmpty)
                {
                    // Retorna null si se permite vacio
                    return null;
                }

                Console.WriteLine(_invalidInputMessage);
                continue;
            }

            // Intentar parsear el decimal
            if (decimal.TryParse(strValue, out decimal number))
            {
                return number;
            }

            Console.WriteLine("Debes ingresar un numero valido.");
        }
    }

    public static bool ReadRequiredBool(string? prompt)
    {
        while (true)
        {
            // Imprimir un prompt inicial si se le pasa
            if (!string.IsNullOrEmpty(prompt))
            {
                Console.Write(prompt);
            }

            // Solicitar el string, y le aplicamos trim y lo convertimos a minus
            var strValue = Console.ReadLine()?.Trim().ToLower();

            // Si dice q si, retornamos true
            if (strValue == "y")
            {
                return true;
            }

            // Si dice q no, retornamos false
            if (strValue == "n")
            {
                return false;
            }

            // SI llego hasta aqui, quiere decir que no es valido, simplemente mostramos el mensaje
            Console.WriteLine("Debes ingresar solamente 'y' o 'n'.");
        }
    }

    public class ReadRequiredDateTimeArgs
    {
        public string Format { get; set; } = "dd/MM/yyyy";
        public string Culture { get; set; } = "es-ES";
    }

    public static DateTime ReadRequiredDateTime(string? prompt, ReadRequiredDateTimeArgs args)
    {
        while (true)
        {
            // Imprimir un prompt inicial si se le pasa
            if (!string.IsNullOrEmpty(prompt))
            {
                Console.Write(prompt);
            }

            string? strValue = Console.ReadLine()?.Trim();

            // Intentar parsear el DateTime
            if (
                DateTime.TryParseExact(
                    strValue,
                    args.Format, // "dd/MM/yyyy"
                    new CultureInfo(args.Culture), // "es-ES"
                    DateTimeStyles.None,
                    out DateTime dateTime
                )
            )
            {
                return dateTime;
            }

            Console.WriteLine("Debes ingresar una fecha valida.");
        }
    }
}
