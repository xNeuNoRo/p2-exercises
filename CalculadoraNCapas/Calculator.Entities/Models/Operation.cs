using Calculator.Entities.Enums;

namespace Calculator.Entities.Models;

public class Operation
{
    public decimal A { get; set; }
    public decimal B { get; set; }
    public OperationType Type { get; set; }
    public decimal Result { get; set; }
}
