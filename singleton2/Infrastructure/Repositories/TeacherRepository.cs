using App.Core.Repositories;
using App.Domain;

namespace App.Infrastructure.Repositories;

public class TeacherRepository : BaseRepo<Teacher>, IRepository<Teacher, int> // Implementa la interfaz IRepository con la clave primaria de tipo int
{
    // Constructor solamente para inicializar el padre con la ruta del archivo
    public TeacherRepository(string filePath)
        : base(filePath) { }

    public void Add(Teacher entity) => Append(entity);

    public Teacher? GetById(int id) => Find(t => t.Id == id);

    public IReadOnlyList<Teacher> GetAll() => Load();

    public bool Delete(int id) => Delete(t => t.Id == id);

    public bool Update(Teacher entity) => Update(t => t.Id == entity.Id, entity);
}
