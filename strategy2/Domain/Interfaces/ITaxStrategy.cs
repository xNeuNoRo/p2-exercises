namespace App.Domain.Interfaces;

public interface ITaxStrategy
{
    decimal Calculate(decimal amount);
}
