using Ejercicio1.Users;
using Ejercicio1.Utils;

namespace Ejercicio1.Factories;

public static class UserFactory
{
    public static User Create(string username, int age, int salary, bool retired)
    {
        if (!Validators.isValidAge(age))
            throw new ArgumentException("Debes ingresar una edad valida!");

        return new User(username, age, salary, retired);
    }
}
