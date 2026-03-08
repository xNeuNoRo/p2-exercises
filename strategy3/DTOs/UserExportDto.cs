namespace App.DTOs;

public class UserExportDto
{
    public string? Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public string? Correo { get; set; }

    public UserExportDto() { }
}
