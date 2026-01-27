using App.Controllers;
using App.Domain;
using App.Helpers;

namespace App.Views;

public class SubjectView
{
    private readonly SubjectController _subjectController;
    private readonly TeacherController _teacherController;

    private static void PressEnterToContinue()
    {
        Console.WriteLine("\nPresiona [Enter] para continuar...");
        Console.ReadLine();
    }

    public SubjectView(SubjectController subjectController, TeacherController teacherController)
    {
        _subjectController = subjectController;
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
                    MenuTitle = "Modulo de Materias",
                    Choices =
                    [
                        "Crear Materia",
                        "Ver todas las Materias",
                        "Regresar al Menu Principal",
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
                    HandleCreateSubject();
                    break;
                }
                case 1:
                {
                    HandleViewAllSubjects();
                    break;
                }
            }
        }
    }

    // ================================
    // Handlers de opciones
    // ================================

    private void HandleCreateSubject()
    {
        Console.Clear();
        Console.WriteLine("=== Crear Nueva Materia ===\n");

        _subjectController.CreateSubject();

        Console.WriteLine("Materia creada exitosamente!");
        PressEnterToContinue();
    }

    // Handler para ver todas las materias
    private void HandleViewAllSubjects()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Materias ===\n");

            var subjects = _subjectController.GetSubjects();

            if (subjects.Count == 0)
            {
                Console.WriteLine("No hay materias registradas actualmente.\n");
                PressEnterToContinue();
                return;
            }

            int selectedSubjectIdx = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Lista de Materias",
                    Choices = subjects
                        .Select(s =>
                            $"Id: {s.Id} | Nombre: {s.Name} | Créditos: {s.Credits} | Estudiante(s) Inscrito(s): {s.StudentIds.Count}"
                        )
                        .ToArray(),
                }
            );

            // SI el usuario selecciona -1, salimos del menu de materias
            if (selectedSubjectIdx == -1)
            {
                break;
            }

            // Obtener la materia seleccionada
            var selectedSubject = subjects[selectedSubjectIdx];

            // Manejar acciones sobre la materia seleccionada
            HandleSubjectActions(selectedSubject);
        }
    }

    private void HandleSubjectActions(Subject subject)
    {
        // Guardar el Id actual de la materia para refrescarla en cada iteración
        int currentSubjectId = subject.Id;

        while (true)
        {
            // Refrescar la materia desde la base de datos
            var refreshedSubject = _subjectController.GetSubject(currentSubjectId);
            if (refreshedSubject == null)
            {
                Console.WriteLine("La materia ya no existe.");
                return;
            }
            subject = refreshedSubject;

            int choice = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Acciones Disponibles",
                    Choices =
                    [
                        "Actualizar Materia",
                        "Eliminar Materia",
                        "Asignar Profesor",
                        "Remover Profesor",
                        "Ver Estudiantes",
                        "Regresar a la Lista de Materias",
                    ],
                }
            );

            switch (choice)
            {
                case -1:
                case 5:
                {
                    return; // Regresar a la lista de materias
                }
                case 0:
                {
                    HandleUpdateSubject(subject);
                    break;
                }
                case 1:
                {
                    _subjectController.DeleteSubjectById(subject.Id);
                    break;
                }
                case 2:
                {
                    HandleAssignTeacherToSubject(subject);
                    break;
                }
                case 3:
                {
                    HandleUnassignTeacherFromSubject(subject);
                    break;
                }
                case 4:
                {
                    HandleViewStudentsInSubject(subject);
                    break;
                }
            }
        }
    }

    // Handler para actualizar una materia
    private void HandleUpdateSubject(Subject subject)
    {
        Input.ReadRequiredStrArgs strArgs = new Input.ReadRequiredStrArgs { AllowEmpty = true };

        Console.Clear();
        Console.WriteLine("=== Actualizar Materia ===\n");

        // Pedir nuevos datos (permitiendo dejar en blanco para no cambiar)
        string name = Input.ReadRequiredStr(
            $"Ingresa el nuevo nombre de la materia (dejar en blanco para no cambiar): ",
            strArgs
        );
        if (string.IsNullOrWhiteSpace(name))
        {
            name = subject.Name;
        }
        int? credits = Input.ReadRequiredInt(
            $"Ingresa los nuevos créditos de la materia (dejar en blanco para no cambiar): ",
            new Input.ReadRequiredIntArgs { AllowEmpty = true, MinValue = 0 }
        );
        if (credits == null)
        {
            credits = subject.Credits;
        }

        // Actualizar la materia
        subject.UpdateInfo(name, credits.Value);
        // Guardar los cambios en la base de datos
        bool success = _subjectController.UpdateSubject(subject);

        if (success)
        {
            Console.WriteLine("\nMateria actualizada exitosamente!");
        }
        else
        {
            Console.WriteLine("\nError al actualizar la materia.");
        }

        PressEnterToContinue();
    }

    // Handler para asignar un profesor a una materia
    private void HandleAssignTeacherToSubject(Subject subject)
    {
        Console.Clear();
        Console.WriteLine("=== Asignar Profesor a Materia ===\n");

        // Listar todos los profesores disponibles
        var teachers = _teacherController.GetTeachers().Where(t => subject.TeacherId != t.Id).ToList(); // Filtrar el profesor ya asignado

        if (teachers.Count == 0)
        {
            Console.WriteLine("No hay profesores registrados actualmente.\n");
            PressEnterToContinue();
            return;
        }

        int selectedTeacherIdx = InteractiveMenu.Show(
            new InteractiveMenu.MenuArgs
            {
                MenuTitle = "Selecciona un Profesor para Asignar",
                Choices = teachers
                    .Select(t =>
                        $"Id: {t.Id} | Nombre: {t.Firstname} {t.Lastname} | Email: {t.Email}"
                    )
                    .ToArray(),
            }
        );

        // Si el usuario selecciona -1, salimos del menu de asignacion
        if (selectedTeacherIdx == -1)
        {
            return;
        }

        // Obtener el profesor seleccionado
        var selectedTeacher = teachers[selectedTeacherIdx];

        // Asignar el profesor a la materia
        bool success = _subjectController.AssignTeacherToSubject(subject.Id, selectedTeacher.Id);

        if (success)
        {
            Console.WriteLine(
                $"\nProfesor {selectedTeacher.Firstname} asignado exitosamente a la materia {subject.Name}!\n"
            );
        }
        else
        {
            Console.WriteLine("\nError al asignar el profesor a la materia.\n");
        }

        PressEnterToContinue();
    }

    // Handler para remover un profesor de una materia
    private void HandleUnassignTeacherFromSubject(Subject subject)
    {
        Console.Clear();
        Console.WriteLine("=== Remover Profesor de Materia ===\n");

        if (subject.TeacherId == null)
        {
            Console.WriteLine("Esta materia no tiene un profesor asignado.\n");
            PressEnterToContinue();
            return;
        }

        // Remover el profesor de la materia
        bool success = _subjectController.RemoveTeacherFromSubject(subject.Id);

        if (success)
        {
            Console.WriteLine($"\nProfesor removido exitosamente de la materia {subject.Name}!\n");
        }
        else
        {
            Console.WriteLine("\nError al remover el profesor de la materia.\n");
        }

        PressEnterToContinue();
    }

    // Handler para ver los estudiantes inscritos en una materia
    private void HandleViewStudentsInSubject(Subject subject)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Estudiantes Inscritos en la Materia ===\n");

            var studentIds = subject.StudentIds;

            if (studentIds.Count == 0)
            {
                Console.WriteLine("No hay estudiantes inscritos en esta materia.\n");
                PressEnterToContinue();
                return;
            }

            // Obtener los estudiantes inscritos en la materia
            List<Student> students = _subjectController.GetStudentsInSubject(subject.Id);

            string[] choices = students
                .Select(s =>
                    $"Id: {s.Id} | Nombre: {s.Firstname} {s.Lastname} | Carrera: {s.Career}"
                )
                .Append("Regresar al menu anterior")
                .ToArray();

            int option = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = $"Estudiantes Inscritos en la Materia {subject.Name}",
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
