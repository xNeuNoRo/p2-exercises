using App.Application.DTOs.Profile;

namespace App.Domain.Interfaces;

public interface IProfileService
{
    Task<IEnumerable<ProfileResponseDto>> GetProfilesAsync();
    Task<ProfileResponseDto?> GetProfileByIdAsync(int id);
    Task UpdateProfileAsync(int id, UpdateProfileRequestDto request);
}
