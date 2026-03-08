using App.Domain.Interfaces;

namespace App.Domain.Strategies.Taxes;

public class ContractTaxStrategy : ITaxStrategy
{
    public decimal Calculate(decimal amount)
    {
        return amount * 0.15m;
    }
}
