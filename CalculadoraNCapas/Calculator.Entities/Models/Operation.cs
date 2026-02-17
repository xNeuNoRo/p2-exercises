using Calculator.Entities.Enums;

namespace Calculator.Entities.Models;

public class Operation
{
    public decimal A { get; init; }
    public decimal B { get; init; }
    public OperationType Type { get; init; }
    public decimal Result { get; init; }
    
    public Operation() { }

    public Operation(decimal a, decimal b, OperationType type, decimal result)
    {
        A = a;
        B = b;
        Type = type;
        Result = result;
    }
}
