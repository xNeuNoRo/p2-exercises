using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Interfaces;
using Repository.Models;

namespace Repository.Services
{
    public class EditorialServices : IEditorial
    {
        private readonly IRepository<Editorial> repository;

        public EditorialServices(IRepository<Editorial> repository)
        {
            this.repository = repository;
        }

        public void DeleteEditorial(int id)
        {
            repository.Delete(id);
        }

        public Editorial GetEditorial(int id)
        {
            return repository.GetById(id);
        }

        public List<Editorial> GetEditoriales()
        {
            return repository.GetAll();
        }

        public void SetEditorial(Editorial editorial)
        {
            repository.Set(editorial);
        }

        public void UpdateEditorial(Editorial editorial)
        {
            repository.Update(editorial);
        }
    }
}
