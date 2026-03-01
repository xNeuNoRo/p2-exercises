using App.Application.DTOs.Pet;
using App.Application.Extensions;
using App.Controllers.Base;
using App.Domain.Enums;
using App.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

public class PetsController : BaseApiController
{
    private readonly IPetService _service;

    // Inyectamos el servicio de mascotas a través del constructor
    public PetsController(IPetService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Obtenemos todas las mascotas utilizando el servicio y obtenemos una lista de DTOs de respuesta
        var pets = await _service.GetPetsAsync();
        return Success(pets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        // Obtenemos la mascota por su ID utilizando el servicio y obtenemos el DTO de respuesta
        var pet = await _service.GetPetByIdAsync(id);
        return Success(pet);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePetRequestDto request)
    {
        // Creamos una nueva mascota utilizando el servicio y obtenemos el DTO de respuesta
        var pet = await _service.CreatePetAsync(request);
        // Retornamos un 201 Created con la ruta para obtener la mascota creada y el DTO de respuesta de la mascota creada
        return CreatedSuccess(nameof(GetById), new { id = pet.Id }, pet);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatePetRequestDto request)
    {
        // Actualizamos la mascota utilizando el servicio
        await _service.UpdatePetAsync(id, request);
        return NoContent(); // 204 No Content
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // Eliminamos la mascota utilizando el servicio
        await _service.DeletePetAsync(id);
        return NoContent(); // 204 No Content
    }

    [HttpGet("species")]
    public IActionResult GetSpecies()
    {
        // Obtenemos la lista de especies de mascotas utilizando el enum PetSpecies
        // y la extensión ToSpanishString para obtener su representación en español
        var speciesList = Enum.GetValues<PetSpecies>()
            .Select(s => new SpeciesDropdownDto
            {
                Id = (int)s,
                Name = s.ToSpanishString(), // Usamos la extensión ToSpanishString para obtener el nombre en español de cada especie
            })
            .ToList();

        // Retornamos la lista de especies de mascotas como un DTO de respuesta
        return Success(speciesList);
    }
}
