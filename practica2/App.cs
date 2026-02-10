using App.Domain.Enum;
using App.Domain.Factories;
using App.Domain.Infrastructure;
using App.Helpers;
using App.Models;
using App.Services;

namespace App;

public class RolesApp
{
    private AdminService _adminService;
    private ClientService _clientService;
    private GuestService _guestService;

    public RolesApp()
    {
        UserFactory _factory = new UserFactory();
        Database database = new Database();
        _adminService = new AdminService(_factory, database);
        _clientService = new ClientService(_factory, database);
        _guestService = new GuestService(_factory, database);
    }

    private static void PressEnterToContinue()
    {
        Console.WriteLine("\nPresiona [Enter] para continuar...");
        Console.ReadLine();
    }

    public void Run()
    {
        bool loop = true;

        while (loop)
        {
            int choice = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Roles App",
                    Choices = ["Administrador", "Cliente", "Invitado"],
                }
            );

            UserTypes userType; // Valor por defecto
            switch (choice)
            {
                case 0:
                    userType = UserTypes.Admin;
                    break;
                case 1:
                    userType = UserTypes.Client;
                    break;
                case 2:
                    userType = UserTypes.Guest;
                    break;
                default:
                    Console.WriteLine("Tipo no valido");
                    PressEnterToContinue();
                    continue; // Volver a mostrar el menú si la opción no es válida
            }

            HandleUserActions(userType);
        }
    }

    private void HandleUserActions(UserTypes userType)
    {
        int choice = InteractiveMenu.Show(
            new InteractiveMenu.MenuArgs
            {
                MenuTitle = $"Acciones de {userType}",
                Choices = ["Crear usuario", "Ver usuarios"],
            }
        );

        switch (choice)
        {
            case 0:
            {
                HandleCreateUser(userType);
                break;
            }
            case 1:
            {
                HandleViewUsers(userType);
                break;
            }
        }
    }

    private void HandleCreateUser(UserTypes userType)
    {
        switch (userType)
        {
            case UserTypes.Admin:
                _adminService.Create();
                break;
            case UserTypes.Client:
                _clientService.Create();
                break;
            case UserTypes.Guest:
                _guestService.Create();
                break;
        }
    }

    private void HandleViewUsers(UserTypes userType)
    {
        switch (userType)
        {
            case UserTypes.Admin:
            {
                List<Admin> admins = _adminService.GetAdmins();

                if (admins.Count == 0)
                {
                    Console.WriteLine("No hay administradores registrados.");
                    PressEnterToContinue();
                    return;
                }

                foreach (Admin adm in admins)
                {
                    Console.WriteLine(adm.ToString());
                    Console.WriteLine("-------------------");
                }
                PressEnterToContinue();
                break;
            }
            case UserTypes.Client:
            {
                List<Client> clients = _clientService.GetClients();

                if (clients.Count == 0)
                {
                    Console.WriteLine("No hay clientes registrados.");
                    PressEnterToContinue();
                    return;
                }

                foreach (Client client in clients)
                {
                    Console.WriteLine(client.ToString());
                    Console.WriteLine("-------------------");
                }
                PressEnterToContinue();
                break;
            }
            case UserTypes.Guest:
            {
                List<Guest> guests = _guestService.GetGuests();

                if (guests.Count == 0)
                {
                    Console.WriteLine("No hay invitados registrados.");
                    PressEnterToContinue();
                    return;
                }

                foreach (Guest guest in guests)
                {
                    Console.WriteLine(guest.ToString());
                    Console.WriteLine("-------------------");
                }
                PressEnterToContinue();
                break;
            }
        }
    }
}
