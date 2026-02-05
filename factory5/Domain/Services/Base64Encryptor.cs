using System.Text;
using App.Domain.Contracts;

namespace App.Domain.Services;

public class Base64Encryptor : IEncryptor
{
    public string Encrypt(string input)
    {
        // Obtenemos los bytes desde un string (UTF8)
        var bytes = Encoding.UTF8.GetBytes(input);
        // Convertimos dichos bytes a otro string (Base64)
        return Convert.ToBase64String(bytes);
    }

    public string Decrypt(string input)
    {
        // Obtenemos los bytes desde un string (Base64)
        var bytes = Convert.FromBase64String(input);
        // Convertimos dichos bytes a otro string (UTF8)
        return Encoding.UTF8.GetString(bytes);
    }
}
