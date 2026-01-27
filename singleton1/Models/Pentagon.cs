using App.Domain;

namespace App.Models;

public class Pentagon : BaseShape
{
    public double Side { get; private set; }
    public double Apothem { get; private set; }

    public Pentagon(int id, string color, double side, double apothem)
        : base(id, "Pentagon", color, 5, true)
    {
        Side = side;
        Apothem = apothem;
    }

    public override double GetPerimeter()
    {
        return 5 * Side;
    }

    public override double GetArea()
    {
        return GetPerimeter() * Apothem / 2;
    }
}
