using App.Models;

namespace App.Domain.Factories;

public class ShapeFactory
{
    public Triangle CreateTriangle(
        int id,
        string color,
        double sideA,
        double sideB,
        double sideC
    ) => new Triangle(id, color, sideA, sideB, sideC);

    public Rectangle CreateRectangle(int id, string color, double baseLength, double height) =>
        new Rectangle(id, color, baseLength, height);

    public Pentagon CreatePentagon(int id, string color, double side, double apothem) =>
        new Pentagon(id, color, side, apothem);

    public Hexagon CreateHexagon(int id, string color, double side, double apothem) =>
        new Hexagon(id, color, side, apothem);

    public Circle CreateCircle(int id, string color, double radius) =>
        new Circle(id, color, radius);
}
