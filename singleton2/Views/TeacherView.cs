using App.Controllers;
using App.Domain;
using App.Helpers;

namespace App.Views;

public class TeacherView
{
    private readonly TeacherController _teacherController;

    private static void PressEnterToContinue()
    {
        Console.WriteLine("\nPresiona [Enter] para continuar...");
        Console.ReadLine();
    }

    public TeacherView(TeacherController teacherController)
    {
        _teacherController = teacherController;
    }

    public void ShowMenu()
    {
        // Variable para controlar el regreso al menu principal
        bool backToMainMenu = false;

        while (!backToMainMenu)
        {
            int option = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Modulo de Profesores",
                    Choices =
                    [
                        "Crear Profesor",
                        "Ver Todos los Profesores",
                        "Volver al Menu Principal",
                    ],
                }
            );

            switch (option)
            {
                case -1:
                case 2:
                {
                    backToMainMenu = true;
                    break;
                }
                case 0:
                {
                    HandleCreateTeacher();
                    break;
                }
                case 1:
                {
                    HandleViewAllSubject();
                    break;
                }
            }
        }
    }

    // ================================
    // Handlers de opciones
    // ================================

    // Handler para crear un nuevo profesor
    private void HandleCreateTeacher()
    {
        Console.Clear();
        Console.WriteLine("=== Crear Nuevo Profesor ===\n");

        _teacherController.CreateTeacher();

        Console.WriteLine("\nProfesor creado exitosamente!");
        PressEnterToContinue();
    }

    // Handler para ver todos los profesores
    private void HandleViewAllSubject()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Profesores ===\n");

            var teachers = _teacherController.GetTeachers();

            if (teachers.Count == 0)
            {
                Console.WriteLine("No hay profesores registrados actualmente.");
                PressEnterToContinue();
                return;
            }

            int selectedTeacherIdx = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Lista de Profesores",
                    Choices = teachers
                        .Select(t =>
                            $"Id: {t.Id} | Nombre: {t.Firstname} {t.Lastname} | Email: {t.Email} | Materias Asignadas: {t.SubjectIds.Count}"
                        )
                        .ToArray(),
                }
            );

            // SI el usuario selecciona -1, salimos del menu de profesores
            if (selectedTeacherIdx == -1)
            {
                break;
            }

            // Obtenemos el profesor seleccionado
            var selectedTeacher = teachers[selectedTeacherIdx];

            // Manejar acciones sobre el profesor seleccionado
            HandleTeacherActions(selectedTeacher);
        }
    }

    // Handler para acciones sobre un profesor seleccionado
    private void HandleTeacherActions(Teacher teacher)
    {
        // Guardar el Id actual del profesor para refrescarlo en cada iteración
        int currentTeacherId = teacher.Id;

        while (true)
        {
            // Refrescar el profesor desde la base de datos
            var refreshedTeacher = _teacherController.GetTeacher(currentTeacherId);
            if (refreshedTeacher == null)
            {
                Console.WriteLine("El profesor ya no existe.");
                return;
            }
            teacher = refreshedTeacher;

            int choice = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle =
                        $"Profesor: {teacher.Firstname} {teacher.Lastname} (Id: {teacher.Id})",
                    Choices =
                    [
                        "Actualizar Profesor",
                        "Eliminar Profesor",
                        "Ver Materias Asignadas",
                        "Regresar a la Lista de Profesores",
                    ],
                }
            );

            switch (choice)
            {
                case -1:
                case 3:
                {
                    return; // Regresar a la lista de profesores
                }
                case 0:
                {
                    HandleUpdateTeacher(teacher);
                    break;
                }
                case 1:
                {
                    _teacherController.DeleteTeacherById(teacher.Id);
                    return;
                }
                case 2:
                {
                    HandleViewTeacherSubjects(teacher);
                    break;
                }
            }
        }
    }

    // Handler para actualizar un profesor
    private void HandleUpdateTeacher(Teacher teacher)
    {
        Input.ReadRequiredStrArgs strArgs = new Input.ReadRequiredStrArgs { AllowEmpty = true };
        Console.Clear();
        Console.WriteLine("=== Actualizar Profesor ===\n");

        string firstname = Input.ReadRequiredStr(
            $"Ingresa el nuevo nombre del profesor (dejar en blanco para no cambiar): ",
            strArgs
        );
        if (string.IsNullOrWhiteSpace(firstname))
        {
            firstname = teacher.Firstname;
        }
        string lastname = Input.ReadRequiredStr(
            "Ingresa el nuevo apellido del profesor (dejar en blanco para no cambiar): ",
            strArgs
        );
        if (string.IsNullOrWhiteSpace(lastname))
        {
            lastname = teacher.Lastname;
        }
        string email;
        do
        {
            email = Input.ReadRequiredStr(
                "Ingresa el nuevo email del profesor (dejar en blanco para no cambiar): ",
                strArgs
            );

            // Si el usuario deja en blanco, mantenemos el email actual, si no, validamos el nuevo email sencillamente con una '@'
            if (!string.IsNullOrWhiteSpace(email) && !email.Contains("@"))
            {
                Console.WriteLine("Debes ingresar un email valido!");
            }
        } while (!string.IsNullOrWhiteSpace(email) && !email.Contains("@"));
        if (string.IsNullOrWhiteSpace(email))
        {
            email = teacher.Email;
        }

        // Actualizar el profesor
        teacher.UpdateInfo(firstname, lastname, email);
        // Guardar los cambios en la base de datos
        bool success = _teacherController.UpdateTeacher(teacher);

        if (success)
        {
            Console.WriteLine("\nProfesor actualizado exitosamente!");
        }
        else
        {
            Console.WriteLine("\nError al actualizar el profesor.");
        }

        PressEnterToContinue();
    }

    // Handler para ver las materias asignadas a un profesor
    private void HandleViewTeacherSubjects(Teacher teacher)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(
                $"=== Materias del Profesor: {teacher.Firstname} {teacher.Lastname} ===\n"
            );

            var subjects = _teacherController.GetTeacherSubjects(teacher.Id);

            if (subjects.Count == 0)
            {
                Console.WriteLine("Este profesor no tiene materias asignadas actualmente.");
                PressEnterToContinue();
                return;
            }

            string[] choices = subjects
                .Select(s => $"Id: {s.Id} | Nombre: {s.Name} | Créditos: {s.Credits}")
                .Append("Regresar al menu anterior")
                .ToArray();

            int option = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle =
                        $"Materias Asignadas al Profesor {teacher.Firstname} {teacher.Lastname}",
                    Choices = choices,
                }
            );

            if (option == -1 || option == choices.Length - 1)
            {
                break; // Regresar al menu anterior
            }
        }
    }
}
