using App.Domain.Enums;
using App.Domain.Interfaces;
using App.Domain.Strategies.Taxes;

namespace App.Domain.Factories;

public static class TaxFactory
{
    public static ITaxStrategy GetTaxStrategy(EmployeeType employeeType)
    {
        return employeeType switch
        {
            EmployeeType.FullTime => new FullTimeTaxStrategy(),
            EmployeeType.PartTime => new PartTimeTaxStrategy(),
            EmployeeType.Contractor => new ContractTaxStrategy(),
            _ => throw new ArgumentException("Tipo de empleado no válido"),
        };
    }
}
