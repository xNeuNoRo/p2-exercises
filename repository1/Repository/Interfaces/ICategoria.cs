using Repository.Models;

namespace Repository.Interfaces
{
    public interface ICategoria
    {
        List<Categoria> GetCategorias();
        Categoria GetCategoria(int id);
        void SetCategoria(Categoria categoria);
        void UpdateCategoria (Categoria categoria);
        void DeleteCategoria(int id);
    }
}
