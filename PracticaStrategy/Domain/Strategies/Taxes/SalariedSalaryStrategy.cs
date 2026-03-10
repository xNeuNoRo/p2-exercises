using App.Domain.Interfaces;
using App.Helpers;

namespace App.Domain.Strategies.Taxes;

public class SalariedSalaryStrategy : ISalaryStrategy
{
    public decimal Calculate()
    {
        var salary = AskForSalary();
        return salary;
    }

    private decimal AskForSalary()
    {
        Input.ReadRequiredDecArgs decArgs = new Input.ReadRequiredDecArgs
        {
            AllowEmpty = false
        };
        
        decimal? salary = Input.ReadRequiredDec($"Ingresa el salario: ", decArgs);
        return salary!.Value;
    }
}
