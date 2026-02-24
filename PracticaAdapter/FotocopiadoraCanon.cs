public class FotocopiadoraCanon : IFotoCopiadora
{
    public void Fotocopiar(string documento, int copias)
    {
        for (int i = 0; i < copias; i++)
        {
            Console.WriteLine($"Canon imprimiendo: {documento}");
        }
    }
}
