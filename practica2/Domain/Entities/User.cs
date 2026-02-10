using App.Domain.Enum;

namespace App.Domain.Entities;

public abstract class User
{
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string Type { get; init; }
    public Permissions[] Permissions { get; init; }

    protected User(
        string name,
        string email,
        string password,
        string type,
        Permissions[] permissions
    )
    {
        Name = name;
        Email = email;
        Password = password;
        Type = type;
        Permissions = permissions;
    }
}
