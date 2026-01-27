using App.Controllers;
using App.Domain;
using App.Services;
using App.Helpers;

namespace App.Views;

public class StudentView
{
    private readonly StudentController _studentController;
    private readonly SubjectController _subjectController;

    private static void PressEnterToContinue()
    {
        Console.WriteLine("\nPresiona [Enter] para continuar...");
        Console.ReadLine();
    }

    public StudentView(StudentController studentController, SubjectController subjectController)
    {
        _studentController = studentController;
        _subjectController = subjectController;
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
                    MenuTitle = "Modulo de Estudiantes",
                    Choices =
                    [
                        "Crear Estudiante",
                        "Ver Todos los Estudiantes",
                        "Regresar al Menu Principal",
                    ],
                }
            );

            switch (option)
            {
                case -1:
                case 2:
                    backToMainMenu = true;
                    break;
                case 0:
                {
                    HandleCreateStudent();
                    break;
                }
                case 1:
                {
                    HandleViewAllStudents();
                    break;
                }
            }
        }
    }

    // ================================
    // Handlers de opciones
    // ================================

    // Handler para crear un estudiante
    private void HandleCreateStudent()
    {
        Console.Clear();
        Console.WriteLine("=== Crear Estudiante ===\n");

        _studentController.CreateStudent();

        Console.WriteLine("\nEstudiante creado exitosamente!\n");
        PressEnterToContinue();
    }

    private void HandleViewAllStudents()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Estudiantes ===\n");

            var students = _studentController.GetStudents();

            if (students.Count == 0)
            {
                Console.WriteLine("No hay estudiantes registrados actualmente.\n");
                PressEnterToContinue();
                return;
            }

            int selectedStudentIdx = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Lista de Estudiantes",
                    Choices = students
                        .Select(s =>
                            $"Id: {s.Id} | Nombre: {s.Firstname} | Apellido: {s.Lastname} | Carrera: {s.Career} | Materias inscritas: {s.SubjectIds.Count}"
                        )
                        .ToArray(),
                }
            );

            // SI el usuario selecciona -1, salimos del menu de estudiantes
            if (selectedStudentIdx == -1)
            {
                break;
            }

            // Obtener el estudiante seleccionado
            var selectedStudent = students[selectedStudentIdx];

            // Manejar acciones sobre el producto seleccionado
            HandleStudentActions(selectedStudent);
        }
    }

    // Handler para acciones sobre un estudiante seleccionado
    private void HandleStudentActions(Student student)
    {
        // Guardar el Id actual del estudiante para refrescarlo en cada iteración
        int currentStudentId = student.Id;

        while (true)
        {
            // Refrescar el estudiante desde la base de datos
            var refreshedStudent = _studentController.GetStudent(currentStudentId);
            if (refreshedStudent == null)
            {
                Console.WriteLine("El estudiante ya no existe.");
                return;
            }
            student = refreshedStudent;

            int choice = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = $"Acciones para Estudiante: {student.Firstname} {student.Lastname}",
                    Choices =
                    [
                        "Actualizar Estudiante",
                        "Eliminar Estudiante",
                        "Inscribir Estudiante en Materia",
                        "Desinscribir Estudiante de Materia",
                        "Ver Materias del Estudiante",
                        "Volver al listado de Estudiantes",
                    ],
                }
            );

            switch (choice)
            {
                case -1:
                case 5:
                    return; // Volver al listado de estudiantes
                case 0:
                {
                    HandleUpdateStudent(student);
                    break;
                }
                case 1:
                {
                    _studentController.DeleteStudentById(student.Id);
                    break;
                }
                case 2:
                {
                    HandleStudentEnroll(student);
                    break;
                }
                case 3:
                {
                    HandleStudentUnenroll(student);
                    break;
                }
                case 4:
                {
                    HandleViewStudentSubjects(student);
                    break;
                }
            }
        }
    }

    // Handler para actualizar un estudiante
    private void HandleUpdateStudent(Student student)
    {
        Input.ReadRequiredStrArgs strArgs = new Input.ReadRequiredStrArgs { AllowEmpty = true };

        Console.Clear();
        Console.WriteLine("=== Actualizar Estudiante ===\n");

        // Pedir nuevos datos (permitiendo dejar en blanco para no cambiar)
        string firstname = Input.ReadRequiredStr(
            "Ingresa el nuevo nombre del estudiante (dejar en blanco para no cambiar): ",
            strArgs
        );
        if (string.IsNullOrWhiteSpace(firstname))
        {
            firstname = student.Firstname;
        }
        string lastname = Input.ReadRequiredStr(
            "Ingresa el nuevo apellido del estudiante (dejar en blanco para no cambiar): ",
            strArgs
        );
        if (string.IsNullOrWhiteSpace(lastname))
        {
            lastname = student.Lastname;
        }
        string career = Input.ReadRequiredStr(
            "Ingresa la nueva carrera del estudiante (dejar en blanco para no cambiar): ",
            strArgs
        );
        if (string.IsNullOrWhiteSpace(career))
        {
            career = student.Career;
        }

        // Actualizar el estudiante
        student.UpdateInfo(firstname, lastname, career);
        // Guardar los cambios en la base de datos
        bool success = _studentController.UpdateStudent(student);

        if (success)
        {
            Console.WriteLine("\nEstudiante actualizado exitosamente!\n");
        }
        else
        {
            Console.WriteLine("\nError al actualizar el estudiante.\n");
        }

        PressEnterToContinue();
    }

    // Handler para inscribir un estudiante en una materia
    private void HandleStudentEnroll(Student student)
    {
        Console.Clear();
        Console.WriteLine("=== Inscribir Estudiante en Materia ===\n");

        var subjects = _subjectController
            .GetSubjects()
            .Where(s => !student.SubjectIds.Contains(s.Id))
            .ToList(); // Filtrar materias ya inscritas

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
                    .Select(s => $"Id: {s.Id} | Nombre: {s.Name} | Créditos: {s.Credits}")
                    .ToArray(),
            }
        );

        // Si el usuario selecciona -1, salimos del handler
        if (selectedSubjectIdx == -1)
        {
            return;
        }

        // Obtener la materia seleccionada
        var selectedSubject = subjects[selectedSubjectIdx];

        // Inscribir al estudiante en la materia
        bool success = _studentController.EnrollStudentInSubject(student.Id, selectedSubject.Id);

        if (success)
        {
            Console.WriteLine(
                $"\nEstudiante {student.Firstname} {student.Lastname} inscrito en la materia {selectedSubject.Name} exitosamente!\n"
            );
        }
        else
        {
            Console.WriteLine(
                $"\nNo se pudo inscribir al estudiante {student.Firstname} {student.Lastname} en la materia {selectedSubject.Name}. Puede que ya esté inscrito.\n"
            );
        }

        PressEnterToContinue();
    }

    // Handler para desinscribir un estudiante de una materia
    private void HandleStudentUnenroll(Student student)
    {
        Console.Clear();
        Console.WriteLine("=== Desinscribir Estudiante de Materia ===\n");

        var subjects = _studentController.GetStudentSubjects(student.Id);

        if (subjects.Count == 0)
        {
            Console.WriteLine("El estudiante no está inscrito en ninguna materia actualmente.\n");
            PressEnterToContinue();
            return;
        }

        int selectedSubjectIdx = InteractiveMenu.Show(
            new InteractiveMenu.MenuArgs
            {
                MenuTitle = "Materias Inscritas",
                Choices = subjects
                    .Select(s => $"Id: {s.Id} | Nombre: {s.Name} | Créditos: {s.Credits}")
                    .ToArray(),
            }
        );

        // Si el usuario selecciona -1, salimos del handler
        if (selectedSubjectIdx == -1)
        {
            return;
        }

        // Obtener la materia seleccionada
        var selectedSubject = subjects[selectedSubjectIdx];

        // Desinscribir al estudiante de la materia
        bool success = _studentController.DropStudentFromSubject(student.Id, selectedSubject.Id);

        if (success)
        {
            Console.WriteLine(
                $"\nEstudiante {student.Firstname} {student.Lastname} desinscrito de la materia {selectedSubject.Name} exitosamente!\n"
            );
        }
        else
        {
            Console.WriteLine(
                $"\nNo se pudo desinscribir al estudiante {student.Firstname} {student.Lastname} de la materia {selectedSubject.Name}.\n"
            );
        }

        PressEnterToContinue();
    }

    // Handler para ver las materias de un estudiante
    private void HandleViewStudentSubjects(Student student)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Materias del Estudiante ===\n");

            var subjects = _studentController.GetStudentSubjects(student.Id);

            if (subjects.Count == 0)
            {
                Console.WriteLine(
                    "El estudiante no está inscrito en ninguna materia actualmente.\n"
                );
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
                    MenuTitle = $"Materias de {student.Firstname} {student.Lastname}",
                    Choices = choices,
                }
            );

            if (option == -1 || option == subjects.Count - 1)
            {
                return; // Regresar al menu anterior
            }
        }
    }
}
