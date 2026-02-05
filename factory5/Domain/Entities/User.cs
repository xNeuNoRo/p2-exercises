namespace App.Domain.Entities;

public class User
{
    public int Id { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }

    // Constructor vacio para el repo
    public User() { }

    public User(int id, string email, string password)
    {
        Id = id;
        Email = email;
        Password = password;
    }

    public override string ToString()
    {
        // En una app real no se deberia mostrar la pass por mas hasheada o encriptada q este
        // pero idk solo es un ejercicio
        return $"[Usuario {Id}] => Email: {Email} | Password: {Password}";
    }
}
