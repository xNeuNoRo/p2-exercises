using App.Core.Repositories;
using App.Domain;

namespace App.Infrastructure.Repositories;

public class SubjectRepository : BaseRepo<Subject>, IRepository<Subject, int> // Implementa la interfaz IRepository con la clave primaria de tipo int
{
    // Constructor solamente para inicializar el padre con la ruta del archivo
    public SubjectRepository(string filePath)
        : base(filePath) { }

    public void Add(Subject entity) => Append(entity);

    public Subject? GetById(int id) => Find(s => s.Id == id);

    public IReadOnlyList<Subject> GetAll() => Load();

    public bool Delete(int id) => Delete(s => s.Id == id);

    public bool Update(Subject entity) => Update(s => s.Id == entity.Id, entity);
}
