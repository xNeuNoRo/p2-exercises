namespace App.DTOs;

public class EmployeeExportDto
{
    public string Nombre { get; set; } = string.Empty;
    public string TipoEmpleado { get; set; } = string.Empty;
    public decimal Salario { get; set; }
    public decimal Impuesto { get; set; }

    public EmployeeExportDto() { }
}
