namespace App.Domain.Interfaces;

public interface IRemovable
{
    public Task<bool> DeleteAsync(string filePath, int id);
}
