using System.Security.Cryptography;
using App.Domain.Contracts;

namespace App.Domain.Services;

public class DesEncryptor : IEncryptor
{
    // Clave (necesaria para cifrar y descifrar)
    private static readonly byte[] _key = Convert.FromBase64String("jK5mN7xP9wU=");

    // Vector de inicializacion (necesario para cifrar y descifrar)
    private static readonly byte[] _iv = Convert.FromBase64String("vR4tZ6xY2bM=");

    public string Encrypt(string input)
    {
        // Creamos un objeto DES
        using var des = DES.Create();

        // Configuramos la clave y el vector de inicializacion
        des.Key = _key;
        des.IV = _iv;

        // Creamos un encriptador usando el objeto DES previamente configurado
        var encryptor = des.CreateEncryptor(des.Key, des.IV);

        // Creamos un stream en memoria para almacenar los datos cifrados de forma temporal (asi no guardamos en disco)
        using var memoryStream = new MemoryStream();
        // Creamos un stream de cifrado, que se encargara de cifrar los datos que se escriban en el stream en memoria
        // Le pasamos el stream en memoria, el encriptador y el modo de operacion (en este caso escritura ya que vamos a escribir datos)
        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        // Usamos un stream de escritura para escribir los datos en el stream de cifrado, que a su vez se encargara de cifrarlos y escribirlos en el stream en memoria
        using (var streamWriter = new StreamWriter(cryptoStream))
        {
            // Finalmente escribimos el string de entrada en el stream de escritura
            streamWriter.Write(input);
        }

        // Si llegamos hasta aqui es porque ya se han cifrado los datos y estan en el stream en memoria
        // Devolvemos los datos del stream en memoria como un string en Base64
        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public string Decrypt(string input)
    {
        // Convertimos el string de entrada de Base64 (salida del encriptador) a un array de bytes
        byte[] buffer = Convert.FromBase64String(input);

        // Creamos un objeto DES
        using var des = DES.Create();

        // Configuramos la clave y el vector de inicializacion
        des.Key = _key;
        des.IV = _iv;

        // Creamos un desencriptador usando el objeto DES previamente configurado
        var decryptor = des.CreateDecryptor(des.Key, des.IV);

        // Creamos un stream en memoria con los bytes del string que queremos descifrar
        using var memoryStream = new MemoryStream(buffer);
        // Creamos un stream de cifrado, que se encargara de descifrar los datos que se lean del stream en memoria
        // Le pasamos el stream en memoria, el desencriptador y el modo de operacion (en este caso lectura ya que vamos a leer datos)
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        // Usamos un stream de lectura para leer los datos del stream de cifrado, que a su vez se encargara de descifrarlos al leerlos del stream en memoria
        using var streamReader = new StreamReader(cryptoStream);

        // Finalmente leemos el contenido del stream de lectura (que ya estara descifrado) y lo devolvemos como string
        return streamReader.ReadToEnd();
    }
}
