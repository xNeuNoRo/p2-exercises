using System.Text.Json;
using App.Domain.Contracts;
using App.Domain.Entities;

namespace App.Services;

public class StudentService
{
    // Campo privado para el repositorio de estudiantes,
    // usando la interfaz IFileRepo<Student> para mantener la abstraccion tal cual
    private readonly IFileRepo<Student> _repo;

    // Inyectamos la dependencia del repositorio a traves del constructor
    public StudentService(IFileRepo<Student> studentRepo)
    {
        _repo = studentRepo;
    }

    // Metodo para obtener todos los estudiantes, delegando al repositorio
    public List<Student> GetAllStudents()
    {
        return _repo.ReadData();
    }

    // Metodo para obtener todos los estudiantes como JSON
    public string GetAllStudentsAsJson()
    {
        var students = _repo.ReadData();
        return JsonSerializer.Serialize(
            students,
            new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }
        );
    }

    // Metodo para obtener un estudiante por su Id, delegando al repositorio
    public Student? GetStudentById(string id)
    {
        // Usamos el metodo Find del repositorio para buscar el estudiante por su Id
        return _repo.Find(s => s.Id == id);
    }

    // Metodo para agregar un nuevo estudiante, delegando al repositorio
    public void AddStudent(Student student)
    {
        _repo.Append(student);
    }
}
