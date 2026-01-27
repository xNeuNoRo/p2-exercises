using App.Domain;

namespace App.Models;

public class Triangle : BaseShape
{
    private static Triangle? _instance;

    public double SideA { get; private set; }
    public double SideB { get; private set; }
    public double SideC { get; private set; }

    private Triangle(int id, string color, double sideA, double sideB, double sideC)
        : base(id, "Triangle", color, 3, false)
    {
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
    }

    public static Triangle GetInstance(
        int id,
        string color,
        double sideA,
        double sideB,
        double sideC
    )
    {
        if (_instance == null)
        {
            _instance = new Triangle(id, color, sideA, sideB, sideC);
        }
        return _instance;
    }

    public override double GetPerimeter()
    {
        return SideA + SideB + SideC;
    }

    public override double GetArea()
    {
        double s = GetPerimeter() / 2;
        return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
    }
}
