using App.Domain.Interfaces;

namespace App.Domain.Strategies;

public class LabradorStrategy : IPetWalkerStrategy
{
    public (decimal price, int durationInMinutes) GetPriceAndDuration()
    {
        return (200.00m, 40);
    }
}
