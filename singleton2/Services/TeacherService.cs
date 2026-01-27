using App.Domain;
using App.Infrastructure;
using App.Helpers;

namespace App.Services;

public class TeacherService
{
    private readonly Database _database;

    // Genera el siguiente Id disponible para un nuevo profesor
    private int GetNextId()
    {
        var teachers = _database.Teachers.GetAll();
        return teachers.Count == 0 ? 1 : teachers.Max(s => s.Id) + 1;
    }

    // Constructor que recibe la instancia de la base de datos
    public TeacherService(Database database)
    {
        _database = database;
    }

    // ================================
    // CRUD (Create, Read, Update, Delete)
    // ================================

    // Crea un nuevo profesor solicitando los datos por consola
    public void Create()
    {
        var (firstname, lastname, email) = GetTeacherData();
        _database.Teachers.Add(new Teacher(GetNextId(), firstname, lastname, email));
    }

    // Solicita los datos del profesor por consola (utilidad privada)
    private (string firstname, string lastname, string email) GetTeacherData()
    {
        Input.ReadRequiredStrArgs strArgs = new Input.ReadRequiredStrArgs { AllowEmpty = false };

        string firstname = Input.ReadRequiredStr("Ingresa el nombre del profesor: ", strArgs);
        string lastname = Input.ReadRequiredStr("Ingresa el apellido del profesor: ", strArgs);
        string email;
        do
        {
            email = Input.ReadRequiredStr("Ingresa el email del profesor: ", strArgs);

            if (!email.Contains("@"))
            {
                Console.WriteLine("Debes ingresar un email valido!");
            }
        } while (!email.Contains("@"));

        return (firstname, lastname, email);
    }

    // Obtener todos los profesores
    public List<Teacher> GetAllTeachers() => _database.Teachers.GetAll().ToList();

    // Obtener un profesor por Id
    public Teacher? GetTeacherById(int id) => _database.Teachers.GetById(id);

    // Actualizar un profesor
    public bool UpdateTeacher(Teacher updatedTeacher) => _database.Teachers.Update(updatedTeacher);

    // Eliminar un profesor por Id
    public bool DeleteById(int id)
    {
        // Antes de eliminar el profesor, desasignarlo de las materias que tenga asignadas
        var subjects = _database.Subjects.GetAll();
        foreach (var subject in subjects.Where(s => s.TeacherId == id))
        {
            subject.RemoveTeacher();
            _database.Subjects.Update(subject);
        }

        return _database.Teachers.Delete(id);
    }

    // ================================
    // Logica de negocio
    // ================================

    // Obtener las materias asignadas a un profesor
    public List<Subject> GetTeacherSubjects(int teacherId)
    {
        var subjects = _database.Subjects.GetAll();
        return subjects.Where(s => s.TeacherId != null && s.TeacherId == teacherId).ToList();
    }
}
