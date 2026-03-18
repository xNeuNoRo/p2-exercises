using Repository.Models;

namespace Repository.Interfaces
{
    public interface IAutor
    {
        List<Autor> GetAutores();
        Autor GetAutor(int id);
        void SetAutor(Autor autor);
        void UpdateAutor(Autor autor);
        void DeleteAutor(int id);
    }
}
