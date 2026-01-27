// Importo mis clases
using Ejercicio1.Factories;
using Ejercicio1.Users;
using Ejercicio1.Utils;

namespace Ejercicio1;

static class Program
{
    // Listado de usuarios (readonly para que la referencia sea inmutable)
    private static readonly List<User> users = new List<User>();

    // Opciones del menu
    private static readonly string[] options = ["Crear un usuario", "Mostrar usuarios"];

    public static void Main(string[] args)
    {
        // Simple bucle para mostrar el menu principal
        // hasta que el usuario desee salir
        while (true)
        {
            // Imprimimos el menu
            MenuUtils.PrintMenu(options);
            int option = InputUtils.ReadRequiredInt("Opcion > ");

            if (!Validators.isValidOption(option, options.Length))
            {
                Console.WriteLine("Debes ingresar una opcion valida del menu!");
                Console.WriteLine("Presiona [Enter] para conTinuar");
                Console.ReadLine();
            }

            // Opcion fija que imprime el menu es 0,
            // la cual es para salir.
            if (option == 0)
                break;

            handleAction(option);
        }
    }

    // Manejamos la accion de la opcion seleccionada
    private static void handleAction(int optionSelected)
    {
        // Aqui orquestamos toda la logica de cada opcion
        switch (optionSelected)
        {
            case 1:
            {
                while (true)
                {
                    Console.WriteLine();

                    // Obtener los parametros de creacion
                    var (username, age, salary, retired) = getUserParams();

                    // Creamos el usuario
                    User createdUser = UserFactory.Create(username, age, salary, retired);

                    // Lo agregamos a la lista
                    users.Add(createdUser);

                    Console.WriteLine();
                    bool confirm = InputUtils.ReadRequiredBool("Deseas crear otro usuario (y/n): ");
                    if (!confirm)
                    {
                        // Limpiamos la consola
                        Console.Clear();

                        // Salimos del bucle
                        break;
                    }
                }

                break;
            }

            case 2:
            {
                // Si no hay usuarios, mostramos un mensaje y ya
                if (users.Count == 0)
                {
                    Console.WriteLine("\nNo hay ningun usuario registrado.\n");
                    return;
                }

                // Limpiamos la consola
                Console.Clear();

                // Sino, iteramos mostrando su info
                for (int i = 0; i < users.Count; i++)
                {
                    // Usuario
                    User user = users[i];

                    // Header
                    Console.WriteLine($"---- Usuario {i + 1} ----");

                    // Imprimimos los detalles del usuario
                    MenuUtils.PrintUserDetails(user);

                    // Footer
                    Console.WriteLine("---------------------");

                    // Espacio en blanco
                    Console.WriteLine();
                }

                Console.WriteLine("Presiona [Enter] para continuar");
                Console.ReadLine();
                break;
            }
        }
    }

    // Esto seria un simple helper para aislar lo que seria la logica
    // de solicitar los parametros necesarios para creacion de un usuario
    private static (string username, int age, int salary, bool retired) getUserParams()
    {
        string username = InputUtils.ReadRequiredStr("Ingrese el nombre de usuario: ");

        // Solicitar la edad hasta que sea una edad valida
        int age;
        do
        {
            age = InputUtils.ReadRequiredInt("Ingrese la edad del usuario: ");

            // Si no es una edad valida, entonces le mostramos el mensaje y el while hara el resto
            if (!Validators.isValidAge(age))
            {
                Console.WriteLine("Debes ingresar una edad valida!");
            }
        } while (!Validators.isValidAge(age));

        int salary = InputUtils.ReadRequiredInt("Ingrese el salario del usuario: ");
        bool retired = InputUtils.ReadRequiredBool("El usuario se encuentra jubilado (y/n): ");

        return (username, age, salary, retired);
    }
}
