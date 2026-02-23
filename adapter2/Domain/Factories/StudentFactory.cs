using App.Domain.Entities;

namespace App.Domain.Factories;

public class StudentFactory
{
    public Student Create(string id, string name, string email, string career)
    {
        return new Student
        {
            Id = id,
            Name = name,
            Email = email,
            Career = career,
        };
    }
}
