using Repository.Context;
using Repository.Interfaces;
using Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Services
{
    public class AutorServices
    {
        private readonly IRepository<Autor> repository;

        public AutorServices(IRepository<Autor> repository)
        {
            this.repository = repository;
        }

        public void Crear(Autor autor)
        {
            repository.Set(autor);
        }
        public void Actualizar(Autor autor)
        {
            repository.Update(autor);
        }
        public Autor Get(int id)
        {
            return repository.GetById(id);
        }
        public List<Autor> GetAll()
        {
            return repository.GetAll();
        }

        public void Eliminar(int id)
        { 
            repository.Delete(id);
        }


        //BibliotecaContext context;
        //public AutorServices(BibliotecaContext context)
        //{
        //    this.context = context;
        //}
        //public void DeleteAutor(int id)
        //{
        //    var entidad = GetAutor(id);
        //    context.Remove(entidad);
        //    context.SaveChanges();
        //}

        //public Autor GetAutor(int id)
        //{
        //    return context.Autor.Find(id);
        //}

        //public List<Autor> GetAutores()
        //{
        //    return context.Autor.ToList();
        //}

        //public void SetAutor(Autor autor)
        //{
        //    context.Add(autor);
        //    context.SaveChanges();
        //}

        //public void UpdateAutor(Autor autor)
        //{
        //    context.Attach(autor);
        //    context.Entry(autor).State = EntityState.Modified;
        //    context.SaveChanges();
        //}
    }
}
