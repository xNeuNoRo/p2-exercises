namespace App.Domain.Interfaces;

public interface IPetWalkerStrategy
{
    public (decimal price, int durationInMinutes) GetPriceAndDuration();
}
