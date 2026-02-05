namespace App.Domain.Contracts;

public interface IEncryptor
{
    string Encrypt(string input);
    string Decrypt(string input);
}
