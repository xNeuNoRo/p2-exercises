using App.Domain.Entities;
using App.Domain.Enum;
using App.Models;

namespace App.Domain.Factories;

public class UserFactory
{
    public User CreateUser(
        string name,
        string email,
        string password,
        string type,
        Permissions[] permissions
    )
    {
        return type switch
        {
            "Admin" => new Admin(name, email, password, permissions),
            "Cliente" => new Client(name, email, password, permissions),
            "Invitado" => new Guest(name, email, password, permissions),
            _ => throw new ArgumentException("Tipo de usuario no válido"),
        };
    }
}
