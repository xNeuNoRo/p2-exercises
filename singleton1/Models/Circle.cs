using App.Domain;

namespace App.Models;

public class Circle : BaseShape
{
    public double Radius { get; private set; }

    public Circle(int id, string color, double radius)
        : base(id, "Circle", color, 0, true)
    {
        Radius = radius;
    }

    public override double GetArea()
    {
        return Math.PI * Radius * Radius;
    }

    public override double GetPerimeter()
    {
        return 2 * Math.PI * Radius;
    }
}
