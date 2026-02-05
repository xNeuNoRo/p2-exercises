namespace App.Domain.Entities;

public class Student
{
    // El Id es la matricula
    public string Id { get; init; }
    public string Name { get; init; }
    public string Career { get; init; }

    // Constructor vacio para los repos
    public Student() { }

    public Student(string id, string name, string career)
    {
        Id = id;
        Name = name;
        Career = career;
    }
}
