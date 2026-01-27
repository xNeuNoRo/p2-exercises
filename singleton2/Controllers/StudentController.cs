using App.Domain;
using App.Services;

namespace App.Controllers;

public class StudentController
{
    private readonly StudentService _studentService;

    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }

    // ================================
    // CRUD (Create, Read, Update, Delete)
    // ================================

    public void CreateStudent() => _studentService.Create();

    public List<Student> GetStudents() => _studentService.GetAllStudents();

    public Student? GetStudent(int id) => _studentService.GetStudentById(id);

    public bool UpdateStudent(Student student) => _studentService.UpdateStudent(student);

    public void DeleteStudentById(int id) => _studentService.DeleteById(id);

    // ================================
    // Logica de negocio
    // ================================

    public bool EnrollStudentInSubject(int studentId, int subjectId) =>
        _studentService.EnrollSubject(studentId, subjectId);

    public bool DropStudentFromSubject(int studentId, int subjectId) =>
        _studentService.UnenrollSubject(studentId, subjectId);

    public List<Subject> GetStudentSubjects(int studentId) =>
        _studentService.GetStudentSubjects(studentId);
}
