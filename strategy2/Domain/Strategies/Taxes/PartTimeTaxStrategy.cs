using App.Domain.Interfaces;

namespace App.Domain.Strategies.Taxes;

public class PartTimeTaxStrategy : ITaxStrategy
{
    public decimal Calculate(decimal amount)
    {
        return amount * 0.10m;
    }
}
