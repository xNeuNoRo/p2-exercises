using App.Domain.Interfaces;

namespace App.Domain.Services;

public class TaxCalculator : ITaxCalculator
{
    private readonly ITaxStrategy _taxStrategy;

    public TaxCalculator(ITaxStrategy taxStrategy)
    {
        _taxStrategy = taxStrategy;
    }

    public decimal CalculateTax(decimal amount)
    {
        return _taxStrategy.Calculate(amount);
    }
}
