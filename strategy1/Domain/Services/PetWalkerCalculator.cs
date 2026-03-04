using App.Domain.Interfaces;

namespace App.Domain.Services;

// Clase contexto
public class PetWalkerCalculator
{
    private readonly IPetWalkerStrategy _petWalkerStrategy;

    public PetWalkerCalculator(IPetWalkerStrategy petWalkerStrategy)
    {
        _petWalkerStrategy = petWalkerStrategy;
    }


    public (decimal price, int durationInMinutes) GetPlan()
    {
        return _petWalkerStrategy.GetPriceAndDuration();
    }
}
