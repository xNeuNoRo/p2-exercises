using App.Domain;
using App.Infrastructure;
using App.Helpers;

namespace App.Services;

public class StudentService
{
    private readonly Database _database;

    // Genera el siguiente Id disponible para un nuevo estudiante
    private int GetNextId()
    {
        var students = _database.Students.GetAll();
        return students.Count == 0 ? 1 : students.Max(s => s.Id) + 1;
    }

    // Constructor que recibe la instancia de la base de datos
    public StudentService(Database database)
    {
        _database = database;
    }

    // ================================
    // CRUD (Create, Read, Update, Delete)
    // ================================

    // Crea un nuevo estudiante solicitando los datos por consola
    public void Create()
    {
        var (firstname, lastname, career) = GetStudentData();
        _database.Students.Add(new Student(GetNextId(), firstname, lastname, career));
    }

    // Solicita los datos del estudiante por consola (utilidad privada)
    private (string firstname, string lastname, string career) GetStudentData()
    {
        Input.ReadRequiredStrArgs strArgs = new Input.ReadRequiredStrArgs { AllowEmpty = false };

        string firstname = Input.ReadRequiredStr("Ingresa el nombre del estudiante: ", strArgs);
        string lastname = Input.ReadRequiredStr("Ingresa el apellido del estudiante: ", strArgs);
        string career = Input.ReadRequiredStr("Ingresa la carrera del estudiante: ", strArgs);

        return (firstname, lastname, career);
    }

    // Obtener todos los estudiantes
    public List<Student> GetAllStudents() => _database.Students.GetAll().ToList();

    // Obtener un estudiante por Id
    public Student? GetStudentById(int id) => _database.Students.GetById(id);

    // Actualizar un estudiante
    public bool UpdateStudent(Student updatedStudent) => _database.Students.Update(updatedStudent);

    // Eliminar un estudiante por Id
    public bool DeleteById(int id)
    {
        // Primero desinscribir al estudiante de todas las materias
        var subjects = _database.Subjects.GetAll();
        foreach (var subject in subjects.Where(s => s.StudentIds.Contains(id)))
        {
            subject.UnenrollStudent(id);
            _database.Subjects.Update(subject);
        }

        return _database.Students.Delete(id);
    }

    // ================================
    // Logica de negocio
    // ================================

    // Inscribir un estudiante a una materia
    public bool EnrollSubject(int studentId, int subjectId)
    {
        var student = _database.Students.GetById(studentId);
        var subject = _database.Subjects.GetById(subjectId);

        // Si no existen el estudiante o la materia
        if (student == null || subject == null)
            return false;

        // Si el estudiante ya está inscrito en la materia
        if (student.SubjectIds.Contains(subjectId))
            return false;

        // Inscribir al estudiante en la materia
        student.EnrollSubject(subject.Id);
        subject.EnrollStudent(student.Id);

        // Actualizar ambos registros en la base de datos
        bool subjectUpdated = _database.Subjects.Update(subject);
        bool studentUpdated = _database.Students.Update(student);

        // Retornamos true solo si ambas actualizaciones fueron exitosas
        return subjectUpdated && studentUpdated;
    }

    // Desinscribir a un estudiante de una materia
    public bool UnenrollSubject(int studentId, int subjectId)
    {
        var student = _database.Students.GetById(studentId);
        var subject = _database.Subjects.GetById(subjectId);

        // Si no existen el estudiante o la materia
        if (student == null || subject == null)
            return false;

        // Si el estudiante no está inscrito en la materia
        if (!student.SubjectIds.Contains(subjectId))
            return false;

        // Desinscribir al estudiante de la materia
        student.DropSubject(subject.Id);
        subject.UnenrollStudent(student.Id);

        // Actualizar ambos registros en la base de datos
        bool studentUpdated = _database.Students.Update(student);
        bool subjectUpdated = _database.Subjects.Update(subject);

        // Retornamos true solo si ambas actualizaciones fueron exitosas
        return subjectUpdated && studentUpdated;
    }

    // Obtener las materias de un estudiante
    public List<Subject> GetStudentSubjects(int studentId)
    {
        var student = _database.Students.GetById(studentId);
        if (student == null)
        {
            throw new ArgumentException("Estudiante no encontrado");
        }

        return student
            .SubjectIds.Select(id => _database.Subjects.GetById(id)) // Obtener cada materia por Id
            .OfType<Subject>() // Filtrar nulos en caso de que alguna materia no exista
            .ToList(); // Convertir a lista
    }
}
