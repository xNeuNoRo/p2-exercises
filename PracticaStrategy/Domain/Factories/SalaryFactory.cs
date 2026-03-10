using App.Domain.Enums;
using App.Domain.Interfaces;
using App.Domain.Strategies.Taxes;

namespace App.Domain.Factories;

public static class SalaryFactory
{
    public static ISalaryStrategy GetSalaryStrategy(EmployeeType employeeType)
    {
        return employeeType switch
        {
            EmployeeType.Salaried => new SalariedSalaryStrategy(),
            EmployeeType.PerHour => new PerHourSalaryStrategy(),
            EmployeeType.Commission => new CommissionSalaryStrategy(),
            _ => throw new ArgumentException("Tipo de empleado no válido"),
        };
    }
}
