using App.Domain.Interfaces;
using App.Helpers;

namespace App.Domain.Strategies.Taxes;

public class CommissionSalaryStrategy : ISalaryStrategy
{
    public decimal Calculate()
    {
        var (salary, totalSales, commissionRate) = AskForSalaryAndSales();
        return salary + (totalSales * commissionRate);
    }

    private (decimal _salary, decimal _totalSales, decimal _commissionRate) AskForSalaryAndSales()
    {
        Input.ReadRequiredDecArgs decArgs = new Input.ReadRequiredDecArgs
        {
            AllowEmpty = false
        };

        decimal? salary = Input.ReadRequiredDec($"Ingresa el salario base: ", decArgs);
        decimal? totalSales = Input.ReadRequiredDec($"Ingresa las ventas totales: ", decArgs);
        decimal? commissionRate = Input.ReadRequiredDec($"Ingresa el porcentaje de comisión: ", decArgs) / 100;

        return (salary!.Value, totalSales!.Value, commissionRate!.Value);
    }
}
