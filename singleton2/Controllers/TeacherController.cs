using App.Domain;
using App.Services;

namespace App.Controllers;

public class TeacherController
{
    public readonly TeacherService _teacherService;

    public TeacherController(TeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    // ================================
    // CRUD (Create, Read, Update, Delete)
    // ================================

    public void CreateTeacher() => _teacherService.Create();

    public List<Teacher> GetTeachers() => _teacherService.GetAllTeachers();

    public Teacher? GetTeacher(int id) => _teacherService.GetTeacherById(id);

    public bool UpdateTeacher(Teacher teacher) => _teacherService.UpdateTeacher(teacher);

    public bool DeleteTeacherById(int id) => _teacherService.DeleteById(id);

    // ================================
    // Logica de negocio
    // ================================

    public List<Subject> GetTeacherSubjects(int teacherId) =>
        _teacherService.GetTeacherSubjects(teacherId);
}
