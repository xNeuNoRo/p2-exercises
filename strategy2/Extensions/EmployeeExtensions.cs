using App.Domain.Entities;
using App.DTOs;

namespace App.Extensions;

public static class EmployeeExtensions
{
    public static EmployeeExportDto ToExportDto(this Employee employee)
    {
        return new EmployeeExportDto
        {
            Nombre = employee.Name ?? "Sin Nombre",
            TipoEmpleado = employee.Type.ToString(),
            Salario = employee.Salary ?? 0m,
            Impuesto = employee.Tax ?? 0m,
        };
    }
}
