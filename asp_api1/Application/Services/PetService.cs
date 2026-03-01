using App.Application.DTOs.Pet;
using App.Domain.Entities;
using App.Domain.Interfaces;
using App.Entities.Exceptions;
using Mapster;

namespace App.Application.Services;

public class PetService : IPetService
{
    private readonly IPetRepository _repository;

    // Inyectamos el repositorio de mascotas a través del constructor
    // para poder utilizarlo en los métodos del servicio
    public PetService(IPetRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Obtiene todas las mascotas utilizando el repositorio y las convierte a DTOs de respuesta.
    /// </summary>
    /// <returns>
    /// Una colección de objetos PetResponseDto que representan las mascotas obtenidas.
    /// </returns>
    public async Task<IEnumerable<PetResponseDto>> GetPetsAsync()
    {
        // Obtenemos todas las mascotas utilizando el repositorio
        var pets = await _repository.GetAllAsync();

        // Convertimos la colección de entidades Pet a una colección de DTOs PetResponseDto
        return pets.Adapt<IEnumerable<PetResponseDto>>();
    }

    /// <summary>
    /// Obtiene una mascota por su ID utilizando el repositorio y la convierte a un DTO de respuesta.
    /// </summary>
    /// <param name="id">
    /// El identificador único de la mascota a obtener.
    /// </param>
    /// <returns>
    /// Un objeto PetResponseDto que representa la mascota obtenida, o null si no se encuentra.
    /// </returns>
    public async Task<PetResponseDto?> GetPetByIdAsync(int id)
    {
        // Obtenemos la mascota por su ID utilizando el repositorio
        var pet = await _repository.GetByIdAsync(id);

        // Si la mascota no existe, lanzamos una excepción personalizada indicando que no se encontró
        if (pet == null)
        {
            throw AppException.NotFound(
                "No se encontró la mascota especificada.",
                ErrorCodes.PetNotFound
            );
        }

        // Convertimos la entidad Pet a un DTO PetResponseDto y lo retornamos
        return pet.Adapt<PetResponseDto>();
    }

    /// <summary>
    /// Crea una nueva mascota utilizando los datos proporcionados en el DTO de solicitud CreatePetRequestDto,
    /// </summary>
    /// <param name="request">
    /// El DTO de solicitud que contiene los datos de la nueva mascota.
    /// </param>
    /// <returns>
    /// Un objeto PetResponseDto que representa la mascota creada.
    /// </returns>
    public async Task<PetResponseDto> CreatePetAsync(CreatePetRequestDto request)
    {
        // Convertimos el DTO de solicitud CreatePetRequestDto a una entidad Pet
        var pet = request.Adapt<Pet>();

        // Agregamos la nueva mascota utilizando el repositorio
        var createdPet = await _repository.AddAsync(pet);

        // Retornamos el DTO de respuesta de la mascota creada
        return createdPet.Adapt<PetResponseDto>();
    }

    /// <summary>
    /// Actualiza una mascota existente utilizando su ID y los datos proporcionados en el DTO de solicitud UpdatePetRequestDto,
    /// si la mascota no existe, se lanza una excepción personalizada indicando que no se encontró.
    /// </summary>
    /// <param name="id">
    /// El identificador único de la mascota a actualizar.
    /// </param>
    /// <param name="request">
    /// El DTO de solicitud que contiene los nuevos datos de la mascota.
    /// </param>
    /// <returns>
    /// Una task que representa la operación asincrónica de actualización de la mascota.
    /// </returns>
    public async Task UpdatePetAsync(int id, UpdatePetRequestDto request)
    {
        // Obtenemos la mascota existente por su ID utilizando el repositorio
        var existingPet = await _repository.GetByIdAsync(id);

        // Si la mascota no existe, lanzamos una excepción personalizada indicando que no se encontró
        if (existingPet == null)
        {
            throw AppException.NotFound(
                "No se encontró la mascota especificada.",
                ErrorCodes.PetNotFound
            );
        }

        // Actualizamos las propiedades de la mascota existente con los datos del DTO de solicitud
        request.Adapt(existingPet);

        // Actualizamos la mascota utilizando el repositorio
        await _repository.UpdateAsync(existingPet);
    }

    /// <summary>
    /// Elimina una mascota por su ID utilizando el repositorio, si la mascota no existe, se lanza una excepción personalizada indicando que no se encontró.
    /// </summary>
    /// <param name="id">
    /// El identificador único de la mascota a eliminar.
    /// </param>
    /// <returns>
    /// Una task que representa la operación asincrónica de eliminación de la mascota.
    /// </returns>
    public async Task DeletePetAsync(int id)
    {
        // Intentamos eliminar la mascota utilizando el repositorio
        var deleted = await _repository.DeleteAsync(id);

        // Si la eliminación no fue exitosa (por ejemplo, porque la mascota no existe), lanzamos una excepción personalizada
        if (!deleted)
        {
            throw AppException.NotFound(
                "No se encontró la mascota especificada.",
                ErrorCodes.PetNotFound
            );
        }
    }
}
