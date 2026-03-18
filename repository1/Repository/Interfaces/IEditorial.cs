using Repository.Models;

namespace Repository.Interfaces
{
    public interface IEditorial
    {
        List<Editorial> GetEditoriales();
        Editorial GetEditorial(int id);
        void SetEditorial(Editorial editorial);
        void UpdateEditorial(Editorial editorial);
        void DeleteEditorial(int id);
    }
}