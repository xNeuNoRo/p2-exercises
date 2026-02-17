using Contracts;

namespace Adapters;

public class AdaptadorRelojAnalogico : IReloj
{
    private readonly RelojAnalogico _relojAnalogico;

    public AdaptadorRelojAnalogico()
    {
        DateTime now = DateTime.Now;
        _relojAnalogico = new RelojAnalogico(now.Hour, now.Minute, now.Second);
    }

    public string ObtenerHoraActual()
    {
        int hora = _relojAnalogico.ObtenerHora();
        int minutos = _relojAnalogico.ObtenerMinutos();
        int segundos = _relojAnalogico.ObtenerSegundos();

        return $"{hora:D2}:{minutos:D2}:{segundos:D2}";
    }
}
