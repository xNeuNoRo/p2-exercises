using App.Infrastructure.Repositories;

namespace App.Infrastructure;

// Sealed para evitar herencias adicionales, solo es un contenedor de repositorios
public sealed class Database
{
    private static Database? _instance;
    public static Database Instance =>
        _instance ??= new Database(Path.Combine(AppContext.BaseDirectory, "Data"));

    public StudentRepository Students { get; private set; }
    public TeacherRepository Teachers { get; private set; }
    public SubjectRepository Subjects { get; private set; }

    private Database(string dataDir)
    {
        Directory.CreateDirectory(dataDir);

        Students = new StudentRepository(Path.Combine(dataDir, "Students.txt"));
        Teachers = new TeacherRepository(Path.Combine(dataDir, "Teachers.txt"));
        Subjects = new SubjectRepository(Path.Combine(dataDir, "Subjects.txt"));
    }
}
