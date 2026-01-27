using App.Domain;
using App.Services;

namespace App.Controllers;

public class SubjectController
{
    private readonly SubjectService _subjectService;

    public SubjectController(SubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    // ================================
    // CRUD (Create, Read, Update, Delete)
    // ================================

    public void CreateSubject() => _subjectService.Create();

    public List<Subject> GetSubjects() => _subjectService.GetAllSubjects();

    public Subject? GetSubject(int id) => _subjectService.GetSubjectById(id);

    public bool UpdateSubject(Subject subject) => _subjectService.UpdateSubject(subject);

    public bool DeleteSubjectById(int id) => _subjectService.DeleteById(id);

    // ================================
    // Logica de negocio
    // ================================

    public bool AssignTeacherToSubject(int subjectId, int teacherId) =>
        _subjectService.AssignTeacherToSubject(subjectId, teacherId);

    public bool RemoveTeacherFromSubject(int subjectId) =>
        _subjectService.UnassignTeacherFromSubject(subjectId);

    public List<Subject> GetSubjectsByTeacher(int teacherId) =>
        _subjectService.GetSubjectsByTeacher(teacherId);

    public List<Student> GetStudentsInSubject(int subjectId) =>
        _subjectService.GetStudentsFromSubject(subjectId);
}
