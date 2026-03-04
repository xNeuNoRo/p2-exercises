using App.Domain.Interfaces;

namespace App.Domain.Strategies;

public class ChihuahuaStrategy : IPetWalkerStrategy
{
    public (decimal price, int durationInMinutes) GetPriceAndDuration()
    {
        return (100.00m, 20);
    }
}
