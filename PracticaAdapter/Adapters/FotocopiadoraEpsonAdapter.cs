public class FotocopiadoraEpsonAdapter : IFotoCopiadora
{
    private readonly FotocopiadoraEpson _fotocopiadoraEpson;

    public FotocopiadoraEpsonAdapter()
    {
        _fotocopiadoraEpson = new FotocopiadoraEpson();
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

        for (int i = 0; i < copiasAImprimir; i++)
        {
            _fotocopiadoraEpson.Copiar(documentoAImprimir);
        }
    }
}
