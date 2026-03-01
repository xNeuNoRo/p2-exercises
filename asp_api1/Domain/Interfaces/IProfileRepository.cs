using App.Domain.Entities;

namespace App.Domain.Interfaces;

public interface IProfileRepository
{
    Task<IEnumerable<Profile>> GetAllAsync();
    Task<Profile?> GetByIdAsync(int id);
    Task UpdateAsync(Profile profile);
}
