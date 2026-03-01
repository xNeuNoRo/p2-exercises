using App.Application.DTOs.Profile;
using App.Controllers.Base;
using App.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

public class ProfilesController : BaseApiController
{
    private readonly IProfileService _service;

    // Inyectamos el servicio de perfiles a través del constructor para poder utilizarlo en los métodos del controlador
    public ProfilesController(IProfileService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Obtenemos todos los perfiles utilizando el servicio y obtenemos una lista de DTOs de respuesta
        var profiles = await _service.GetProfilesAsync();
        return Success(profiles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        // Obtenemos el perfil por su ID utilizando el servicio y obtenemos el DTO de respuesta
        var profile = await _service.GetProfileByIdAsync(id);
        return Success(profile);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProfileRequestDto request)
    {
        // Actualizamos el perfil utilizando el servicio
        await _service.UpdateProfileAsync(id, request);
        return NoContent(); // 204 No Content
    }
}
