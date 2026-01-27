using App.Domain;

namespace App.Models;

public class Square : BaseShape
{
    public double Side { get; private set; }

    public Square(int id, string color, double side)
        : base(id, "Cuadrado", color, 4, true)
    {
        Side = side;
    }

    public override double GetArea()
    {
        return Side * Side;
    }

    public override double GetPerimeter()
    {
        return 4 * Side;
    }
}
