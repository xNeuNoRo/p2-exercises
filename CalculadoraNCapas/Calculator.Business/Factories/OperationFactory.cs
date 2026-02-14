using Calculator.Entities.Enums;
using Calculator.Entities.Models;

namespace Calculator.Business.Factories;

public class OperationFactory
{
    public Operation Create(decimal a, decimal b, OperationType type)
    {
        if (type == OperationType.Division && b == 0)
            throw new InvalidOperationException("No se puede dividir por cero.");

        decimal result = type switch
        {
            OperationType.Addition => a + b,
            OperationType.Subtraction => a - b,
            OperationType.Multiplication => a * b,
            OperationType.Division => a / b,
            _ => throw new InvalidOperationException("Tipo de operacion no soportada."),
        };

        return new Operation
        {
            A = a,
            B = b,
            Type = type,
            Result = result,
        };
    }
}
