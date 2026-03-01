using App.Application.DTOs.Profile;
using App.Domain.Interfaces;
using App.Entities.Exceptions;
using Mapster;

namespace App.Application.Services;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository _repository;

    // Inyectamos el repositorio de perfiles a través del constructor para poder acceder a los datos de los perfiles
    public ProfileService(IProfileRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProfileResponseDto>> GetProfilesAsync()
    {
        // Obtenemos todos los perfiles desde el repositorio
        var profiles = await _repository.GetAllAsync();

        // Convertimos la colección de entidades Profile a una colección de DTOs ProfileResponseDto y la retornamos
        return profiles.Adapt<IEnumerable<ProfileResponseDto>>();
    }

    public async Task<ProfileResponseDto?> GetProfileByIdAsync(int id)
    {
        // Verificamos si el perfil existe antes de intentar obtenerlo
        var profile = await _repository.GetByIdAsync(id);

        // Si el perfil no existe, lanzamos una excepción personalizada indicando que no se encontró
        if (profile == null)
        {
            throw AppException.NotFound(
                "No se encontró el perfil especificado.",
                ErrorCodes.ProfileNotFound
            );
        }

        // Convertimos la entidad Profile a un DTO ProfileResponseDto y lo retornamos
        return profile.Adapt<ProfileResponseDto>();
    }

    public async Task UpdateProfileAsync(int id, UpdateProfileRequestDto request)
    {
        // Verificamos si el perfil existe antes de intentar actualizarlo
        var existingProfile = await _repository.GetByIdAsync(id);

        // Si el perfil no existe, lanzamos una excepción personalizada indicando que no se encontró
        if (existingProfile == null)
        {
            throw AppException.NotFound(
                "No se encontró el perfil especificado.",
                ErrorCodes.ProfileNotFound
            );
        }

        // Actualizamos las propiedades del perfil existente con los datos del DTO de solicitud
        request.Adapt(existingProfile);

        // Guardamos los cambios en el repositorio
        await _repository.UpdateAsync(existingProfile);
    }
}
