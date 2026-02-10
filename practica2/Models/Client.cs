using App.Domain.Entities;
using App.Domain.Enum;

namespace App.Models;

public class Client : User
{
    public Client(string name, string email, string password, Permissions[] permissions)
        : base(name, email, password, "Cliente", permissions) { }

    public override string ToString()
    {
        return "Tipo: "
            + Type
            + "\n"
            + "Nombre: "
            + Name
            + "\n"
            + "Email: "
            + Email
            + "\n"
            + "Permisos: "
            + string.Join(", ", Permissions);
    }
}
