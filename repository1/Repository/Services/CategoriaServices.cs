using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Interfaces;
using Repository.Models;

namespace Repository.Services
{
    public class CategoriaServices : ICategoria
    {
        private readonly IRepository<Categoria> repository;

        public CategoriaServices(IRepository<Categoria> repository)
        {
            this.repository = repository;
        }

        public void DeleteCategoria(int id)
        {
            repository.Delete(id);
        }

        public Categoria GetCategoria(int id)
        {
            return repository.GetById(id);
        }

        public List<Categoria> GetCategorias()
        {
            return repository.GetAll();
        }

        public void SetCategoria(Categoria categoria)
        {
            repository.Set(categoria);
        }

        public void UpdateCategoria(Categoria categoria)
        {
            repository.Update(categoria);
        }
    }
}
