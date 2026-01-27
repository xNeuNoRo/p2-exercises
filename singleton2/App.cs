using App.Controllers;
using App.Infrastructure;
using App.Services;
using App.Views;
using App.Helpers;

namespace App;

public class StudentApp
{
    private readonly StudentView _studentView;
    private readonly SubjectView _subjectView;
    private readonly TeacherView _teacherView;

    // Constructor
    public StudentApp()
    {
        // Inyección de dependencias manual
        var database = Database.Instance;
        var studentService = new StudentService(database);
        var subjectService = new SubjectService(database);
        var teacherService = new TeacherService(database);
        var studentController = new StudentController(studentService);
        var subjectController = new SubjectController(subjectService);
        var teacherController = new TeacherController(teacherService);
        _studentView = new StudentView(studentController, subjectController);
        _subjectView = new SubjectView(subjectController, teacherController);
        _teacherView = new TeacherView(teacherController);
    }

    public void Run()
    {
        // Menu Principal
        bool loop = true;
        while (loop)
        {
            var selectedChoice = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Student App (P2 - Juan Rosario)\nDeveloped By Angel",
                    Choices =
                    [
                        "Modulo de estudiantes",
                        "Modulo de materias",
                        "Modulo de profesores",
                        "Salir del programa",
                    ],
                    IsMainMenu = true,
                }
            );

            switch (selectedChoice)
            {
                case 3:
                case -1:
                    if (HandleExit(selectedChoice == 3))
                    {
                        loop = false;
                        break;
                    }
                    break;
                case 0:
                    _studentView.ShowMenu();
                    break;
                case 1:
                    _subjectView.ShowMenu();
                    break;
                case 2:
                    _teacherView.ShowMenu();
                    break;
            }
        }
    }

    // Maneja la salida del programa, con confirmación si es necesario
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
