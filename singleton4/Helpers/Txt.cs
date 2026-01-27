namespace App.Helpers;

public static class Txt
{
    // Imprime el contenido de un archivo de texto en la consola
    public static void PrintTxt(string _filePath)
    {
        if (File.Exists(_filePath))
        {
            string[] lines = File.ReadAllLines(_filePath);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
        else
        {
            Console.WriteLine("No se encontro el archivo de texto");
        }
    }

    // Agrega una linea a un archivo de texto
    public static void AppendLine(string filePath, string line)
    {
        File.AppendAllText(filePath, line + Environment.NewLine);
    }

    // Asegura que un archivo tenga un header, si no lo tiene o no existe, lo crea
    public static void EnsureHeader(string filePath, string header)
    {
        if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
        {
            File.AppendAllText(filePath, header + Environment.NewLine);
        }
    }
}
