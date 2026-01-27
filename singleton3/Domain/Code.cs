namespace App.Domain;

public class Code
{
    // Singleton
    private static Code? _instance;

    // Atributos del codigo
    private int _counter = 0; // Contador interno
    private readonly string _prefix; // Prefijo del codigo
    private readonly int _width; // Ancho de los "0"

    // Constructor privado
    private Code(string prefix, int width)
    {
        _prefix = prefix;
        _width = width;
    }

    // Metodo singleton
    public static Code GetInstance(string prefix = "CODE", int width = 8)
    {
        if (_instance == null)
        {
            _instance = new Code(prefix, width);
        }

        return _instance;
    }

    // Metodo para generar el siguiente codigo
    public string Count()
    {
        _counter++;
        return $"{_prefix}-{_counter.ToString().PadLeft(_width, '0')}";
    }

    // Metodo para reiniciar el contador
    public void Reset()
    {
        _counter = 0;
    }
}
