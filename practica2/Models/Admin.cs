using App.Domain.Entities;
using App.Domain.Enum;

namespace App.Models;

public class Admin : User
{

    public Admin(string name, string email, string password, Permissions[] permissions)
        : base(name, email, password, "Admin", permissions) { }

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
