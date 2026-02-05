using App.Domain.Entities;

namespace App.Models;

public class Rectangle : BaseShape
{
    public double BaseLength { get; private set; }
    public double Height { get; private set; }

    public Rectangle(int id, string color, double baseLength, double height)
        : base(id, "Rectangle", color, 4, false)
    {
        BaseLength = baseLength;
        Height = height;
    }

    public override double GetArea()
    {
        return BaseLength * Height;
    }

    public override double GetPerimeter()
    {
        return 2 * (BaseLength + Height);
    }
}
