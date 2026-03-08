using App.Domain.Entities;
using App.DTOs;

namespace App.Extensions;

public static class UserExtensions
{
    public static UserExportDto ToExportDto(this User user)
    {
        return new UserExportDto
        {
            Nombre = user.Name ?? "Sin Nombre",
            Edad = user.Age,
            Correo = user.Email ?? "Sin Correo",
        };
    }
}
