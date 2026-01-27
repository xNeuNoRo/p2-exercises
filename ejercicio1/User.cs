namespace Ejercicio1.Users;

public class User
{
    // Atributos
    public string Name { get; private set; }
    public int Age { get; private set; }
    public int Salary { get; private set; }
    public double Savings { get; private set; }
    public bool Retired { get; private set; }

    // Constructor
    public User(string name, int age, int salary, bool retired)
    {
        Name = name;
        Age = age;
        Salary = salary;
        // Si fuera algo mas general y proyecto grande, 
        // se abstraeria a otra clase para no violar el 
        // principio OC (Open-Close)
        Savings = salary * 0.15; 
        Retired = retired;
    }
}
