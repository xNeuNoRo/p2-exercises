using App.Domain.Interfaces;

namespace App.Domain.Services;

public class SalaryCalculator : ISalaryCalculator
{
    private readonly ISalaryStrategy _salaryStrategy;

    public SalaryCalculator(ISalaryStrategy salaryStrategy)
    {
        _salaryStrategy = salaryStrategy;
    }

    public decimal CalculateSalary()
    {
        return _salaryStrategy.Calculate();
    }
}
