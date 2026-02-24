public class FotocopiadoraIndustrialAdapter : IFotoCopiadora
{
    private readonly FotocopiadoraIndustrial _fotocopiadoraIndustrial;

    public FotocopiadoraIndustrialAdapter()
    {
        _fotocopiadoraIndustrial = new FotocopiadoraIndustrial();
    }

    public void Fotocopiar(string documento, int copias)
    {
        Input.ReadRequiredStrArgs strArgs = new Input.ReadRequiredStrArgs { AllowEmpty = false };
        Input.ReadRequiredIntArgs intArgs = new Input.ReadRequiredIntArgs { AllowEmpty = false };

        string documentoAImprimir = Input.ReadRequiredStr(
            "Ingrese el documento que desea imprimir: ",
            strArgs
        );

        int? copiasAImprimir = Input.ReadRequiredInt(
            "Ingrese las copias que desea imprimir: ",
            intArgs
        );

        bool modoAltaVelocidad = Input.ReadRequiredBool(
            "Deseas habilitar el modo alta velocidad (y/n)? "
        );

        for (int i = 0; i < copiasAImprimir; i++)
        {
            _fotocopiadoraIndustrial.HacerCopias(
                copiasAImprimir.Value,
                documentoAImprimir,
                modoAltaVelocidad
            );
        }
    }
}
