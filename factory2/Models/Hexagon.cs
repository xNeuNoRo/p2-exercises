using App.Domain.Entities;

namespace App.Models;

public class Hexagon : BaseShape
{
    public double Side { get; private set; }
    public double Apothem { get; private set; }

    public Hexagon(int id, string color, double side, double apothem)
        : base(id, "Hexagon", color, 6, true)
    {
        Side = side;
        Apothem = apothem;
    }

    public override double GetPerimeter()
    {
        return 6 * Side;
    }

    public override double GetArea()
    {
        return GetPerimeter() * Apothem / 2;
    }
}
