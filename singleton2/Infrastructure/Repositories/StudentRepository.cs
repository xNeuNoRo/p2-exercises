using App.Core.Repositories;
using App.Domain;

namespace App.Infrastructure.Repositories;

public class StudentRepository : BaseRepo<Student>, IRepository<Student, int> // Implementa la interfaz IRepository con la clave primaria de tipo int
{
    // Constructor solamente para inicializar el padre con la ruta del archivo
    public StudentRepository(string filePath)
        : base(filePath) { }

    public void Add(Student entity) => Append(entity);

    public Student? GetById(int id) => Find(s => s.Id == id);

    public IReadOnlyList<Student> GetAll() => Load();

    public bool Delete(int id) => Delete(s => s.Id == id);

    public bool Update(Student entity) => Update(s => s.Id == entity.Id, entity);
}
