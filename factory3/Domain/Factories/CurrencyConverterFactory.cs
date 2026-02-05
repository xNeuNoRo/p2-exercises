using App.Domain.Contracts;
using App.Domain.Enums;
using App.Domain.Services;

namespace App.Domain.Factories;

public class CurrencyConverterFactory
{
    public ICurrencyConverter Create(CurrencyType currencyType)
    {
        return currencyType switch
        {
            CurrencyType.Dollar => new RDToDollarConverter(),
            CurrencyType.Euro => new RDToEuroConverter(),
            CurrencyType.Yen => new RDToYenConverter(),
            _ => throw new NotSupportedException($"La moneda {currencyType} no es soportada."),
        };
    }
}
