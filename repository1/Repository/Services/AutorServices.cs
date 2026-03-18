using Repository.Context;
using Repository.Interfaces;
using Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Services
{
    public class AutorServices : IAutor
    {
        private readonly IRepository<Autor> repository;

        public AutorServices(IRepository<Autor> repository)
        {
            this.repository = repository;
        }

        public void SetAutor(Autor autor)
        {
            repository.Set(autor);
        }
        public void UpdateAutor(Autor autor)
        {
            repository.Update(autor);
        }
        public Autor GetAutor(int id)
        {
            return repository.GetById(id);
        }
        public List<Autor> GetAutores()
        {
            return repository.GetAll();
        }

        public void DeleteAutor(int id)
        { 
            repository.Delete(id);
        }

    }
}
