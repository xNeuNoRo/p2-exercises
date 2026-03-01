using App.Application.DTOs.Pet;

namespace App.Domain.Interfaces;

public interface IPetService
{
    Task<IEnumerable<PetResponseDto>> GetPetsAsync();
    Task<PetResponseDto?> GetPetByIdAsync(int id);
    Task<PetResponseDto> CreatePetAsync(CreatePetRequestDto request);
    Task UpdatePetAsync(int id, UpdatePetRequestDto request);
    Task DeletePetAsync(int id);
}
