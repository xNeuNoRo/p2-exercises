namespace App.Domain.Entities;

// Clase que representara mi perfil en la app
public class Profile
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Career { get; set; }
    public required string StudentId { get; set; }
}
