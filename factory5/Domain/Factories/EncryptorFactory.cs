using App.Domain.Contracts;
using App.Domain.Enums;
using App.Domain.Services;

namespace App.Domain.Factories;

public class EncryptorFactory
{
    // Crear una instancia del encriptador segun el tipo q se le pase
    public IEncryptor CreateEncryptor(EncryptorType type)
    {
        return type switch
        {
            EncryptorType.Base64 => new Base64Encryptor(),
            EncryptorType.Aes => new AesEncryptor(),
            EncryptorType.Des => new DesEncryptor(),
            _ => throw new NotSupportedException(
                $"El tipo de encriptador '{type}' no es soportado."
            ),
        };
    }
}
