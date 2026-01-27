using System.Text.Json.Serialization;

namespace App.Domain;

public class Student
{
    public int Id { get; private set; }
    public string Firstname { get; private set; }
    public string Lastname { get; private set; }
    public string Career { get; private set; }
    public List<int> SubjectIds { get; private set; } = new List<int>();

    public Student(int id, string firstname, string lastname, string career)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        Career = career;
    }

    [JsonConstructor]
    public Student(int id, string firstname, string lastname, string career, List<int> subjectIds)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        Career = career;
        SubjectIds = subjectIds ?? new List<int>();
    }

    public void UpdateInfo(string firstname, string lastname, string career)
    {
        Firstname = firstname;
        Lastname = lastname;
        Career = career;
    }

    public void EnrollSubject(int subjectId)
    {
        SubjectIds.Add(subjectId);
    }

    public void DropSubject(int subjectId)
    {
        SubjectIds.Remove(subjectId);
    }
}
