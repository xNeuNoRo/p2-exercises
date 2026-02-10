using App.Domain.Entities;
using App.Domain.Enum;

namespace App.Models;

public class Guest : User
{
    public Guest(string name, string email, string password, Permissions[] permissions)
        : base(name, email, password, "Invitado", permissions) { }

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
