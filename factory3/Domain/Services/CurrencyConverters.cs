using App.Domain.Contracts;

namespace App.Domain.Services;

public class RDToDollarConverter : ICurrencyConverter
{
    private const decimal ExchangeRate = 0.0157m;

    public decimal Convert(decimal amount)
    {
        return amount * ExchangeRate;
    }
}

public class RDToEuroConverter : ICurrencyConverter
{
    private const decimal ExchangeRate = 0.0134m;

    public decimal Convert(decimal amount)
    {
        return amount * ExchangeRate;
    }
}

public class RDToYenConverter : ICurrencyConverter
{
    private const decimal ExchangeRate = 2.43m;

    public decimal Convert(decimal amount)
    {
        return amount * ExchangeRate;
    }
}
