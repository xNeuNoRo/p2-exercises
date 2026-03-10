using App.Domain.Enums;

namespace App.Domain.Entities;

public class Employee
{
    /// <summary>
    /// Nombre completo del empleado
    /// </summary>
    /// <example>John Doe</example>
    public string? Name { get; set; }

    /// <summary>
    /// Tipo de empleado (FullTime, PartTime, Contractor)
    /// </summary>
    /// <example>FullTime</example>
    public EmployeeType Type { get; set; }

    /// <summary>
    /// Departamento al que pertenece el empleado
    /// </summary>
    /// <example>IT</example>
    public string? Department { get; set; }

    /// <summary>
    /// Salario del empleado
    /// </summary>
    /// <example>50000</example>
    public decimal? Salary { get; set; }

    public Employee() { }

    /// <summary>
    /// Constructor para inicializar un nuevo empleado con sus propiedades
    /// </summary>
    /// <param name="name">Nombre completo del empleado</param>
    /// <param name="type">Tipo de empleado (FullTime, PartTime, Contractor)</param>
    /// <param name="department">Departamento al que pertenece el empleado</param>
    /// <param name="salary">Salario del empleado</param>
    public Employee(
        string name,
        EmployeeType type,
        string department,
        decimal salary
    )
    {
        Name = name;
        Type = type;
        Department = department;
        Salary = salary;
    }

    public override string ToString()
    {
        return $"Nombre: {Name}, Tipo: {Type}, Departamento: {Department}, Salario: {Salary:C}";
    }
}
