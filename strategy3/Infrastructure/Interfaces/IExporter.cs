namespace App.Infrastructure.Interfaces;

public interface IExporter<in T>
    where T : class
{
    Task Export(T entity);
}
