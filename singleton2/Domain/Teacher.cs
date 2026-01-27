using System.Text.Json.Serialization;

namespace App.Domain;

public class Teacher
{
    public int Id { get; private set; }
    public string Firstname { get; private set; }
    public string Lastname { get; private set; }
    public string Email { get; private set; }
    public List<int> SubjectIds { get; private set; } = new List<int>();

    public Teacher(int id, string firstname, string lastname, string email)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
    }

    [JsonConstructor]
    public Teacher(int id, string firstname, string lastname, string email, List<int> subjectIds)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        SubjectIds = subjectIds ?? new List<int>();
    }

    public void UpdateInfo(string firstname, string lastname, string email)
    {
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
    }

    public void AddSubject(int subjectId)
    {
        SubjectIds.Add(subjectId);
    }

    public void RemoveSubject(int subjectId)
    {
        SubjectIds.Remove(subjectId);
    }
}
