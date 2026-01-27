using App.Controllers;
using App.Domain;
using InvoiceApp.Helpers;

namespace App.Views;

public class ShapeView
{
    private readonly ShapeController _controller;

    public ShapeView(ShapeController controller)
    {
        _controller = controller;
    }

    public static void DisplayShapeDetails(BaseShape shape)
    {
        Console.WriteLine($"--- Detalles de la figura (ID: {shape.Id}) ---");
        Console.WriteLine($"Tipo: {shape.Name}");
        Console.WriteLine($"Color: {shape.Color}");
        Console.WriteLine($"Número de lados: {shape.Sides}");
        Console.WriteLine($"Área: {shape.GetArea():F2}");
        Console.WriteLine($"Perímetro: {shape.GetPerimeter():F2}");
    }

    public BaseShape? ShowCreationMenu(int id)
    {
        int selectedChoice = InteractiveMenu.Show(
            new InteractiveMenu.MenuArgs
            {
                MenuTitle = "Selecciona el tipo de figura a crear:",
                Choices =
                [
                    "Círculo",
                    "Triángulo",
                    "Cuadrado",
                    "Rectángulo",
                    "Pentágono",
                    "Hexágono",
                ],
            }
        );

        if (selectedChoice == -1)
        {
            return null;
        }

        ShapeType shapeType = selectedChoice switch
        {
            0 => ShapeType.Circle,
            1 => ShapeType.Triangle,
            2 => ShapeType.Square,
            3 => ShapeType.Rectangle,
            4 => ShapeType.Pentagon,
            5 => ShapeType.Hexagon,
            _ => throw new ArgumentException("Opción no válida."),
        };

        return _controller.CreateShape(id, shapeType);
    }

    public static void ShowShapesList(List<BaseShape> shapes)
    {
        ShapeController.ListShapes(shapes);
    }
}
