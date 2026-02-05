namespace App.Domain.Contracts;

public interface ICurrencyConverter
{
    decimal Convert(decimal amount);
}
