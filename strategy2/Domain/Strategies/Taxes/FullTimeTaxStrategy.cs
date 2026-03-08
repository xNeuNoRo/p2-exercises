using App.Domain.Interfaces;

namespace App.Domain.Strategies.Taxes;

public class FullTimeTaxStrategy : ITaxStrategy
{
    public decimal Calculate(decimal amount)
    {
        return amount * 0.25m;
    }
}
