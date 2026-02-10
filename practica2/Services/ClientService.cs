using App.Domain.Enum;
using App.Domain.Factories;
using App.Domain.Infrastructure;
using App.Helpers;
using App.Models;

namespace App.Services;

public class ClientService
{
    private readonly UserFactory _factory;
    private readonly Database _database;

    public ClientService(UserFactory factory, Database database)
    {
        _factory = factory;
        _database = database;
    }

    private (string username, string email, string password) AskForUserData()
    {
        Input.ReadRequiredStrArgs strArgs = new Input.ReadRequiredStrArgs { AllowEmpty = false };

        string username = Input.ReadRequiredStr("Ingrese el nombre de usuario: ", strArgs);
        string email = Input.ReadRequiredStr("Ingrese el email: ", strArgs);
        string password = Input.ReadRequiredStr("Ingrese la contraseña: ", strArgs);

        return (username, email, password);
    }

    public void Create()
    {
        var (username, email, password) = AskForUserData();

        Client client = (Client)
            _factory.CreateUser(
                username,
                email,
                password,
                "Cliente",
                [Permissions.Buy, Permissions.History, Permissions.UpdateProfile]
            );

        _database.Clients.Add(client);
    }

    public List<Client> GetClients()
    {
        return _database.Clients;
    }
}
