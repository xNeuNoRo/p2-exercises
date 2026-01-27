using App.Domain;
using App.Infrastructure;
using InvoiceApp.Helpers;

namespace App.Services;

public class SubjectService
{
    private readonly Database _database;

    // Genera el siguiente Id disponible para una nueva materia
    private int GetNextId()
    {
        var subjects = _database.Subjects.GetAll();
        return subjects.Count == 0 ? 1 : subjects.Max(s => s.Id) + 1;
    }

    // Constructor que recibe la instancia de la base de datos
    public SubjectService(Database database)
    {
        _database = database;
    }

    // ================================
    // CRUD (Create, Read, Update, Delete)
    // ================================

    // Crea una nueva materia solicitando los datos por consola
    public void Create()
    {
        var (name, credits) = GetSubjectData();
        _database.Subjects.Add(new Subject(GetNextId(), name, credits));
    }

    // Solicita los datos de la materia por consola (utilidad privada)
    private (string name, int credits) GetSubjectData()
    {
        Input.ReadRequiredStrArgs strArgs = new Input.ReadRequiredStrArgs { AllowEmpty = false };
        Input.ReadRequiredIntArgs intArgs = new Input.ReadRequiredIntArgs
        {
            AllowEmpty = false,
            MinValue = 0,
        };

        string name = Input.ReadRequiredStr("Ingresa el nombre de la materia: ", strArgs);
        int? credits = Input.ReadRequiredInt("Ingresa los créditos de la materia: ", intArgs);

        return (name, credits ?? 0);
    }

    // Obtener todas las materias
    public List<Subject> GetAllSubjects() => _database.Subjects.GetAll().ToList();

    // Obtener una materia por Id
    public Subject? GetSubjectById(int id) => _database.Subjects.GetById(id);

    // Actualizar una materia
    public bool UpdateSubject(Subject updatedSubject) => _database.Subjects.Update(updatedSubject);

    // Eliminar una materia por Id
    public bool DeleteById(int id)
    {
        // Obtener la materia
        var subject = _database.Subjects.GetById(id);
        // Si no existe la materia retornar false
        if (subject == null)
            return false;

        // Quitar la materia de la lista del Profesor asignado
        if (subject.TeacherId.HasValue)
        {
            var teacher = _database.Teachers.GetById(subject.TeacherId.Value);
            if (teacher != null)
            {
                teacher.RemoveSubject(id);
                _database.Teachers.Update(teacher);
            }
        }

        // Quitar la materia de la lista de inscripciones de cada Estudiante
        var students = _database.Students.GetAll();
        foreach (var student in students.Where(s => s.SubjectIds.Contains(id)))
        {
            student.DropSubject(id);
            _database.Students.Update(student);
        }

        return _database.Subjects.Delete(id);
    }

    // ================================
    // Logica de negocio
    // ================================

    // Asignar un profesor a una materia
    public bool AssignTeacherToSubject(int subjectId, int teacherId)
    {
        var subject = _database.Subjects.GetById(subjectId);
        var teacher = _database.Teachers.GetById(teacherId);

        // Si no existen la materia o el profesor
        if (subject == null || teacher == null)
        {
            return false;
        }

        // Asignar el profesor a la materia
        subject.AssignTeacher(teacher);
        // Agregar la materia a la lista que imparte el profesor
        teacher.AddSubject(subject.Id);

        // Guardar los cambios en la base de datos
        bool teacherUpdated = _database.Teachers.Update(teacher);
        bool subjectUpdated = _database.Subjects.Update(subject);

        // Retornar si ambas actualizaciones fueron exitosas
        return teacherUpdated && subjectUpdated;
    }

    // Desasignar un profesor de una materia
    public bool UnassignTeacherFromSubject(int subjectId)
    {
        var subject = _database.Subjects.GetById(subjectId);

        // Si no existe la materia o no tiene profesor asignado
        if (subject == null || subject.TeacherId == null)
        {
            return false;
        }

        var teacher = _database.Teachers.GetById(subject.TeacherId.Value);
        if (teacher == null)
        {
            return false;
        }

        // Remover la materia de la lista que imparte el profesor
        teacher.RemoveSubject(subject.Id);
        // Desasignar el profesor de la materia
        subject.RemoveTeacher();

        // Guardar los cambios en la base de datos
        bool teacherUpdated = _database.Teachers.Update(teacher);
        bool subjectUpdated = _database.Subjects.Update(subject);

        // Retornar si ambas actualizaciones fueron exitosas
        return teacherUpdated && subjectUpdated;
    }

    // Obtener las materias de un profesor
    public List<Subject> GetSubjectsByTeacher(int teacherId)
    {
        var teacher = _database.Teachers.GetById(teacherId);
        if (teacher == null)
        {
            throw new ArgumentException("Profesor no encontrado");
        }

        return teacher
            .SubjectIds.Select(id => _database.Subjects.GetById(id)) // Obtener cada materia por Id
            .OfType<Subject>() // Filtrar nulos en caso de que alguna materia no exista
            .ToList(); // Convertir a lista
    }

    // Obtener los estudiantes inscritos en una materia
    public List<Student> GetStudentsFromSubject(int subjectId)
    {
        var subject = _database.Subjects.GetById(subjectId);
        if (subject == null)
        {
            throw new ArgumentException("Materia no encontrada");
        }

        return subject
            .StudentIds.Select(id => _database.Students.GetById(id)) // Obtener cada estudiante por Id
            .OfType<Student>() // Filtrar nulos en caso de que algun estudiante no exista
            .ToList(); // Convertir a lista
    }
}
