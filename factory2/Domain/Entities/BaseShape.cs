namespace App.Domain.Entities;

public abstract class BaseShape
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Color { get; private set; }
    public int Sides { get; private set; }
    public bool IsRegular { get; private set; }

    protected BaseShape(int id, string name, string color, int sides, bool isRegular)
    {
        Id = id;
        Name = name;
        Color = color;
        Sides = sides;
        IsRegular = isRegular;
    }

    public abstract double GetArea();
    public abstract double GetPerimeter();

    public string GetInfo()
    {
        return $"Figura: {Name}, Color: {Color}, Lados: {Sides}, Regular: {IsRegular}";
    }
}
