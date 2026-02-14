using Calculator.Entities.Enums;
using Calculator.Entities.Models;

namespace App.Helpers;

public class OperationDisplay
{
    protected OperationDisplay() { }

    public static string Symbol(OperationType type) =>
        type switch
        {
            OperationType.Addition => "+",
            OperationType.Subtraction => "-",
            OperationType.Multiplication => "*",
            OperationType.Division => "/",
            _ => throw new InvalidOperationException("Tipo de operacion no soportada."),
        };

    public static string FormatToText(Operation op) =>
        $"{op.A} {Symbol(op.Type)} {op.B} = {op.Result}";
}
