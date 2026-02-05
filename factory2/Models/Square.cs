using App.Domain.Entities;

namespace App.Models;

public class Square : BaseShape
{
    private static Square? _instance;
    public double Side { get; private set; }

    private Square(int id, string color, double side)
        : base(id, "Square", color, 4, true)
    {
        Side = side;
    }

    public static Square GetInstance(int id, string color, double side)
    {
        if (_instance == null)
        {
            _instance = new Square(id, color, side);
        }
        return _instance;
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
