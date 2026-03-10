using App.Domain.Interfaces;
using App.Helpers;

namespace App.Domain.Strategies.Taxes;

public class PerHourSalaryStrategy : ISalaryStrategy
{
    public decimal Calculate()
    {
        var (pricePerHour, hoursWorked) = AskForDetails();
        return pricePerHour * hoursWorked;
    }

    private (decimal pricePerHour, decimal hoursWorked) AskForDetails()
    {
        Input.ReadRequiredDecArgs decArgs = new Input.ReadRequiredDecArgs { AllowEmpty = false };

        decimal? pricePerHour = Input.ReadRequiredDec($"Ingresa el precio por hora: ", decArgs);
        decimal? hoursWorked = Input.ReadRequiredDec($"Ingresa las horas trabajadas: ", decArgs);

        return (pricePerHour!.Value, hoursWorked!.Value);
    }
}
