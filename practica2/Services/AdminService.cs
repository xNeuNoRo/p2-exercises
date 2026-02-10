using App.Domain.Enum;
using App.Domain.Factories;
using App.Domain.Infrastructure;
using App.Helpers;
using App.Models;

namespace App.Services;

public class AdminService
{
    private readonly UserFactory _factory;
    private readonly Database _database;

    public AdminService(UserFactory factory, Database database)
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

        Admin admin = (Admin)_factory.CreateUser(username, email, password, "Admin", [Permissions.ManageUsers, Permissions.ReadReports, Permissions.SetupSystem]);

        _database.Admins.Add(admin);
    }

    public List<Admin> GetAdmins()
    {
        return _database.Admins;
    }
}
