namespace App.Domain.Interfaces;

public interface ITaxCalculator
{
    decimal CalculateTax(decimal amount);
}
