using App.Controllers;
using App.Domain;
using App.Services;
using App.Views;
using App.Helpers;

namespace App;

public class ShapeApp
{
    private readonly ShapeView _shapeView;
    private readonly List<BaseShape> _shapes = new List<BaseShape>();

    private int GetNextId()
    {
        if (_shapes.Count == 0)
            return 1;

        // Si hay figuras, buscamos el ID mas alto y le sumamos 1
        return _shapes.Max(i => i.Id) + 1;
    }

    public ShapeApp()
    {
        var service = new ShapeService();
        var controller = new ShapeController(service);
        _shapeView = new ShapeView(controller);
    }

    public void Run()
    {
        bool loop = true;
        while (loop)
        {
            var selectedChoice = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Shapes App (P2 - Juan Rosario)\nDeveloped By Angel Gonzalez",
                    Choices = ["Crear Figura", "Ver Figuras", "Salir"],
                    IsMainMenu = true,
                }
            );

            switch (selectedChoice)
            {
                case -1:
                case 2:
                    if (HandleExit(selectedChoice == 2))
                    {
                        loop = false;
                        break;
                    }
                    break;
                case 0:
                    BaseShape? newShape = _shapeView.ShowCreationMenu(GetNextId());
                    if (newShape == null)
                        continue;

                    if (newShape.Name == "Triangle" && _shapes.Any(s => s.Name == "Triangle"))
                    {
                        Console.WriteLine(
                            "Ya existe un triángulo creado. No se puede crear otro (Singleton)."
                        );
                        Console.WriteLine("Presiona [Enter] para continuar...");
                        Console.ReadLine();
                        break;
                    }
                    _shapes.Add(newShape);
                    break;
                case 1:
                    ShapeView.ShowShapesList(_shapes);
                    break;
            }
        }
    }

    private bool HandleExit(bool shouldConfirm)
    {
        if (shouldConfirm)
        {
            var confirm = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Estas seguro que deseas salir?",
                    Choices = ["Si, deseo salir.", "No, no quiero salir ahora."],
                }
            );

            if (confirm == 0)
            {
                return true;
            }

            return false;
        }
        else
        {
            return true;
        }
    }
}
