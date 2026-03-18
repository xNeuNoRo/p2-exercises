using Repository.Models;

namespace Repository.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Set(T entidad);
        void Update(T entidad);
        void Delete(int id);
    }
}
