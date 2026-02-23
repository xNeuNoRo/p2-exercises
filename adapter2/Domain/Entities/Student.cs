namespace App.Domain.Entities;

public class Student
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Career { get; set; }

    public Student() { }

    public override string ToString()
    {
        return $"Matricula: {Id}, Nombre: {Name}, Email: {Email}, Carrera: {Career}";
    }
}
