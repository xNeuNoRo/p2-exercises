namespace App.Helpers;

public static class Txt
{
    // Muestra el contenido del archivo de texto
    public static void PrintTxt(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("No se encontró el archivo de texto.");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length == 0)
        {
            Console.WriteLine("El archivo está vacío.");
            return;
        }

        foreach (string line in lines)
            Console.WriteLine(line);
    }

    // Muestra el contenido del archivo de texto formateado por encuestas
    public static void PrintSurveyTxt(string filePath)
    {
        // Verificar si el archivo existe
        if (!File.Exists(filePath))
        {
            Console.WriteLine("No se encontró el archivo de texto.");
            return;
        }

        // Leer todas las líneas del archivo
        string[] lines = File.ReadAllLines(filePath);

        // Inicializar variables para el formato
        int survey = 0;
        bool printedHeader = false;
        bool hasContent = false;

        // Recorrer las líneas
        foreach (string line in lines)
        {
            // Si encuentra el separador, reinicia el header
            if (line.Trim() == "---")
            {
                printedHeader = false;
                continue;
            }

            // Saltar líneas vacías
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Imprimir el header de la encuesta si no se ha impreso aún
            if (!printedHeader)
            {
                survey++;
                Console.WriteLine($"\n=== Encuesta {survey} ===");
                printedHeader = true;
            }

            // Imprimir la línea actual
            Console.WriteLine(line);

            // Marcar que hay contenido
            hasContent = true;
        }

        // Si no se imprimió ninguna encuesta, indicar que el archivo está vacío
        if (!hasContent)
            Console.WriteLine("El archivo está vacío.");
    }

    // Agrega una línea al final del archivo
    public static void AppendLine(string filePath, string line)
    {
        File.AppendAllText(filePath, line + Environment.NewLine);
    }

    // Agrega una respuesta completa con el formato del ejercicio
    public static void AppendSurveyResponse(
        string filePath,
        string client,
        int rating,
        string comment
    )
    {
        // Separador entre respuestas (requerimiento)
        EnsureSeparator(filePath);

        AppendLine(filePath, $"Cliente: {client}");
        AppendLine(filePath, $"Calificación: {rating}");
        AppendLine(filePath, $"Comentario: {comment}");
        AppendLine(filePath, "---");
    }

    // Si el archivo ya tiene contenido y NO termina con '---', agrega un separador antes de la nueva respuesta
    private static void EnsureSeparator(string filePath)
    {
        if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
            return;

        string lastLine = File.ReadLines(filePath).LastOrDefault() ?? "";

        if (lastLine.Trim() != "---")
        {
            AppendLine(filePath, "---");
        }
    }
}
