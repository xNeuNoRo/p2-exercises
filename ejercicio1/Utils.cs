using Ejercicio1.Users;

namespace Ejercicio1.Utils;

public static class InputUtils
{
    // Pedir un input string valido
    public static string ReadRequiredStr(string? prompt)
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

            // Si no es valido (nulo o espacios en blanco) entonces le arrojamos el mensaje
            if (string.IsNullOrWhiteSpace(strValue))
            {
                Console.WriteLine("Debes ingresar un valor valido!");
            }

            // El while se encargara de reiniciarlo si es que no es valido
        } while (string.IsNullOrWhiteSpace(strValue));

        // Removemos cualquier posible espacio en blanco con el trim()
        return strValue.Trim();
    }

    // Pedir un input entero valido
    public static int ReadRequiredInt(string? prompt)
    {
        // Inicializamos un bucle infinito hasta que ingrese un num valido
        while (true)
        {
            // Imprimir un prompt inicial si se le pasa
            if (!string.IsNullOrEmpty(prompt))
            {
                Console.Write(prompt);
            }

            // Solicitar el string
            var strValue = Console.ReadLine();

            // Si es valido
            if (int.TryParse(strValue, out int number))
            {
                // Retornamos - ESto sale del bucle autom.
                return number;
            }

            // Si llego hasta aqui es pq no es valido
            Console.WriteLine("Debes ingresar un numero entero valido!");
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
}

public static class Validators
{
    public static bool isValidAge(int age)
    {
        // Simples validaciones
        if (age < 0)
            return false;
        if (age > 120)
            return false;

        return true;
    }

    public static bool isValidOption(int optionInput, int totalOptions)
    {
        // Simples validaciones
        if (optionInput < 0 || optionInput > totalOptions)
            return false;

        return true;
    }
}

public static class MenuUtils
{
    private static void PrintMenuHeader()
    {
        Console.WriteLine("---- Ejercicio 1 ----");
    }

    private static void PrintMenuFooter()
    {
        Console.WriteLine("---------------------");
    }

    public static void PrintMenu(string[] options)
    {
        // Imprimimos el encabezado del menu
        PrintMenuHeader();

        // Imprimimos las opciones
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }
        Console.WriteLine("0. Salir del programa");

        // Imprimimos el footer del menu
        PrintMenuFooter();
    }

    public static void PrintUserDetails(User user)
    {
        Console.WriteLine($"Nombre: {user.Name}");
        Console.WriteLine($"Edad: {user.Age}");
        Console.WriteLine($"Salario: {user.Salary}");
        Console.WriteLine($"Ahorros: {user.Savings}");
        Console.WriteLine(user.Retired ? "Se encuentra jubilado" : "Aun no esta jubilado");
    }
}
