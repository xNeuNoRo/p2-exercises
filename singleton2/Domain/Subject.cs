using System.Text.Json.Serialization;

namespace App.Domain;

public class Subject
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Credits { get; private set; }
    public int? TeacherId { get; private set; }
    public List<int> StudentIds { get; private set; } = new List<int>();

    public Subject(int id, string name, int credits)
    {
        Id = id;
        Name = name;
        Credits = credits;
    }

    [JsonConstructor]
    public Subject(int id, string name, int credits, int? teacherId, List<int> studentIds)
    {
        Id = id;
        Name = name;
        Credits = credits;
        TeacherId = teacherId;
        StudentIds = studentIds ?? new List<int>();
    }

    public void UpdateInfo(string name, int credits)
    {
        Name = name;
        Credits = credits;
    }

    public void AssignTeacher(Teacher teacher)
    {
        TeacherId = teacher.Id;
    }

    public void RemoveTeacher()
    {
        TeacherId = null;
    }

    public void EnrollStudent(int studentId)
    {
        if (!StudentIds.Contains(studentId))
        {
            StudentIds.Add(studentId);
        }
    }

    public void UnenrollStudent(int studentId)
    {
        StudentIds.Remove(studentId);
    }
}
