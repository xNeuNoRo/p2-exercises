public class Aula
{
    private readonly IFotoCopiadora _fotocopiadora;

    public Aula(IFotoCopiadora fotoCopiadora)
    {
        _fotocopiadora = fotoCopiadora;
    }

    public void ReproducirMaterial(string contenido, int cantidad)
    {
        _fotocopiadora.Fotocopiar(contenido, cantidad);
    }
}
