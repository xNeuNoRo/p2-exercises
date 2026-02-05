using App.Domain;
using App.Helpers;
using App.Models;

namespace App.Services;

public class ShapeService
{
    // Dependencies
    private readonly ShapeFactory _shapeFactory;

    // Constructor
    public ShapeService(ShapeFactory shapeFactory)
    {
        _shapeFactory = shapeFactory;
    }

    public BaseShape Create(int id, ShapeType type)
    {
        string color = getCreationShapeArgs();
        return type switch
        {
            ShapeType.Circle => HandleCreateCircle(id, color),
            ShapeType.Triangle => HandleCreateTriangle(id, color),
            ShapeType.Square => HandleCreateSingletonSquare(id, color),
            ShapeType.Rectangle => HandleCreateRectangle(id, color),
            ShapeType.Pentagon => HandleCreatePentagon(id, color),
            ShapeType.Hexagon => HandleCreateHexagon(id, color),
            _ => throw new ArgumentException("Tipo de figura no soportado."),
        };
    }

    public Circle HandleCreateCircle(int id, string color)
    {
        double? radius = Input.ReadRequiredDouble(
            "Radio del círculo: ",
            new Input.ReadRequiredDoubleArgs() { AllowEmpty = false, MinValue = 0.1 }
        );

        return _shapeFactory.CreateCircle(
            id,
            color,
            radius ?? throw new ArgumentException("El radio no puede ser nulo.")
        );
    }

    public Triangle HandleCreateTriangle(int id, string color)
    {
        double? sideA = Input.ReadRequiredDouble(
            "Lado A del triángulo: ",
            new Input.ReadRequiredDoubleArgs() { AllowEmpty = false, MinValue = 0.1 }
        );
        double? sideB = Input.ReadRequiredDouble(
            "Lado B del triángulo: ",
            new Input.ReadRequiredDoubleArgs() { AllowEmpty = false, MinValue = 0.1 }
        );
        double? sideC = Input.ReadRequiredDouble(
            "Lado C del triángulo: ",
            new Input.ReadRequiredDoubleArgs() { AllowEmpty = false, MinValue = 0.1 }
        );

        return _shapeFactory.CreateTriangle(
            id,
            color,
            sideA ?? throw new ArgumentException("El lado A no puede ser nulo."),
            sideB ?? throw new ArgumentException("El lado B no puede ser nulo."),
            sideC ?? throw new ArgumentException("El lado C no puede ser nulo.")
        );
    }

    public Square HandleCreateSingletonSquare(int id, string color)
    {
        double? side = Input.ReadRequiredDouble(
            "Lado del cuadrado: ",
            new Input.ReadRequiredDoubleArgs() { AllowEmpty = false, MinValue = 0.1 }
        );

        return Square.GetInstance(
            id,
            color,
            side ?? throw new ArgumentException("El lado no puede ser nulo.")
        );
    }

    public Rectangle HandleCreateRectangle(int id, string color)
    {
        double? baseLength = Input.ReadRequiredDouble(
            "Base del rectángulo: ",
            new Input.ReadRequiredDoubleArgs() { AllowEmpty = false, MinValue = 0.1 }
        );
        double? height = Input.ReadRequiredDouble(
            "Alto del rectángulo: ",
            new Input.ReadRequiredDoubleArgs() { AllowEmpty = false, MinValue = 0.1 }
        );

        return _shapeFactory.CreateRectangle(
            id,
            color,
            baseLength ?? throw new ArgumentException("El ancho no puede ser nulo."),
            height ?? throw new ArgumentException("El alto no puede ser nulo.")
        );
    }

    public Pentagon HandleCreatePentagon(int id, string color)
    {
        double? side = Input.ReadRequiredDouble(
            "Lado del pentágono: ",
            new Input.ReadRequiredDoubleArgs() { AllowEmpty = false, MinValue = 0.1 }
        );
        double? apothem = Input.ReadRequiredDouble(
            "Apotema del pentágono: ",
            new Input.ReadRequiredDoubleArgs() { AllowEmpty = false, MinValue = 0.1 }
        );

        return _shapeFactory.CreatePentagon(
            id,
            color,
            side ?? throw new ArgumentException("El lado no puede ser nulo."),
            apothem ?? throw new ArgumentException("El apotema no puede ser nulo.")
        );
    }

    public Hexagon HandleCreateHexagon(int id, string color)
    {
        double? side = Input.ReadRequiredDouble(
            "Lado del hexágono: ",
            new Input.ReadRequiredDoubleArgs() { AllowEmpty = false, MinValue = 0.1 }
        );
        double? apothem = Input.ReadRequiredDouble(
            "Apotema del hexágono: ",
            new Input.ReadRequiredDoubleArgs() { AllowEmpty = false, MinValue = 0.1 }
        );

        return _shapeFactory.CreateHexagon(
            id,
            color,
            side ?? throw new ArgumentException("El lado no puede ser nulo."),
            apothem ?? throw new ArgumentException("El apotema no puede ser nulo.")
        );
    }

    private string getCreationShapeArgs()
    {
        Input.ReadRequiredStrArgs strArgs = new Input.ReadRequiredStrArgs() { AllowEmpty = true };

        string color = Input.ReadRequiredStr(
            "Color de la figura (puedes dejarlo vacio): ",
            strArgs
        );
        if (string.IsNullOrWhiteSpace(color))
        {
            color = "Negro";
        }

        return color;
    }
}
