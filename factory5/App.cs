using App.Domain.Entities;
using App.Domain.Enums;
using App.Domain.Factories;
using App.Helpers;
using App.Infrastructure.Repositories;
using StringUtils = App.Helpers.String;

namespace App;

public class EncryptorApp
{
    private readonly EncryptorFactory _factory;
    private readonly JsonRepo<User> _repo;
    private readonly string _filePath = "users";

    private static void PressEnterToContinue()
    {
        Console.WriteLine("\nPresiona [Enter] para continuar...");
        Console.ReadLine();
    }

    public EncryptorApp()
    {
        _factory = new EncryptorFactory();
        _repo = JsonRepo<User>.GetInstance(StringUtils.EnsureExtension(_filePath, "json"));
    }

    public void Run()
    {
        bool loop = true;
        while (loop)
        {
            int selectedChoice = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "App de Encriptacion\nFactory Method Pattern",
                    Choices = ["Crear Usuario", "Listar Usuarios", "Salir"],
                }
            );

            switch (selectedChoice)
            {
                case -1:
                case 2:
                    if (HandleExit(selectedChoice == 2))
                    {
                        loop = false;
                    }
                    break;
                case 0:
                {
                    User newUser = CreateUser();
                    _repo.Append(newUser);
                    Console.WriteLine("Usuario creado exitosamente!");
                    PressEnterToContinue();
                    break;
                }
                case 1:
                {
                    var users = _repo.Load();
                    Console.WriteLine("=== Lista de Usuarios ===\n");
                    if (users.Count == 0)
                    {
                        Console.WriteLine("No hay usuarios registrados.");
                    }
                    foreach (var user in users)
                    {
                        Console.WriteLine(user.ToString());
                    }
                    PressEnterToContinue();
                    break;
                }
            }
        }
    }

    private bool HandleExit(bool shouldConfirm)
    {
        if (shouldConfirm)
        {
            var confirm = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Estas seguro que deseas salir?",
                    Choices = ["Si, deseo salir.", "No, no quiero salir ahora."],
                }
            );

            if (confirm == 0)
            {
                return true;
            }

            return false;
        }
        else
        {
            return true;
        }
    }

    private int GetNextId()
    {
        var users = _repo.Load();

        if (users.Count == 0)
            return 1;

        // Si hay usuarios, buscamos el ID mas alto y le sumamos 1
        return users.Max(i => i.Id) + 1;
    }

    private User CreateUser()
    {
        Input.ReadRequiredStrArgs strArgs = new Input.ReadRequiredStrArgs { AllowEmpty = false };

        string email = Input.ReadRequiredStr("Ingrese el email del usuario: ", strArgs);
        string password = Input.ReadRequiredStr("Ingrese la contraseña del usuario: ", strArgs);
        EncryptorType encryptorType = email switch
        {
            string s when s.EndsWith("@gmail.com") => EncryptorType.Base64,
            string s when s.EndsWith("@hotmail.com") => EncryptorType.Aes,
            string s when s.EndsWith("@itla.edu.do") => EncryptorType.Des,
            _ => EncryptorType.Base64, // Default
        };

        var encryptor = _factory.CreateEncryptor(encryptorType);
        string encryptedPassword = encryptor.Encrypt(password);

        return new User(GetNextId(), email, encryptedPassword);
    }
}
