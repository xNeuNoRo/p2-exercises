using App.Domain.Interfaces;

namespace App.Domain.Strategies;

public class HuskyStrategy : IPetWalkerStrategy
{
    public (decimal price, int durationInMinutes) GetPriceAndDuration()
    {
        return (300.00m, 60);
    }
}
