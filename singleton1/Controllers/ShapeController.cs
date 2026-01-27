using App.Domain;
using App.Services;
using App.Views;

namespace App.Controllers;

public class ShapeController
{
    private readonly ShapeService _shapeService;

    public ShapeController(ShapeService service)
    {
        _shapeService = service;
    }

    public BaseShape CreateShape(int id, ShapeType type)
    {
        return _shapeService.Create(id, type);
    }

    public static void ListShapes(List<BaseShape> shapes)
    {
        if (shapes.Count == 0)
        {
            Console.WriteLine("No hay figuras creadas actualmente.");
            Console.WriteLine("Presiona [Enter] para volver...");
            Console.ReadLine();
            return;
        }

        foreach (var shape in shapes)
        {
            ShapeView.DisplayShapeDetails(shape);
            Console.WriteLine();
        }

        Console.WriteLine("Presiona [Enter] para continuar...");
        Console.ReadLine();
    }
}
