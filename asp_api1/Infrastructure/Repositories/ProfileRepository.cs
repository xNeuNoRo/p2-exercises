using App.Domain.Entities;
using App.Domain.Interfaces;
using App.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly AppDbContext _context;

    // Constructor que recibe el contexto de base de datos a través de inyección de dependencias
    public ProfileRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todos los perfiles del contexto de base de datos de forma asincrónica.
    /// </summary>
    /// <returns>Una lista de objetos Profile.</returns>
    public async Task<IEnumerable<Profile>> GetAllAsync()
    {
        // Utilizamos el método ToListAsync para obtener todos los perfiles de forma asincrónica
        // AsNoTracking se utiliza para mejorar el rendimiento al indicar que no necesitamos
        // realizar un seguimiento de los cambios en las entidades recuperadas
        // Esto es útil para operaciones de solo lectura, como en este caso, donde solo queremos obtener los datos sin modificarlos
        return await _context.Profiles.AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Obtiene un perfil por su ID desde el contexto de base de datos.
    /// </summary>
    /// <param name="id">
    /// El identificador del perfil a obtener.
    /// </param>
    /// <returns>
    /// Un objeto Profile que representa el perfil obtenido, o null si no se encuentra.
    /// </returns>
    public async Task<Profile?> GetByIdAsync(int id)
    {
        // Utilizamos el método FindAsync para buscar un perfil por su ID de forma asincrónica
        return await _context.Profiles.FindAsync(id);
    }

    /// <summary>
    /// Actualiza un perfil existente en el contexto de base de datos y guarda los cambios de forma asincrónica.
    /// </summary>
    /// <param name="profile">
    /// El objeto Profile que contiene los datos actualizados del perfil a modificar.
    /// </param>
    /// <returns>
    /// Una task que representa la operación asincrónica de actualización del perfil.
    /// </returns>
    public async Task UpdateAsync(Profile profile)
    {
        // Actualizamos el perfil existente en el contexto de base de datos
        _context.Profiles.Update(profile);

        // Guardamos los cambios en la base de datos de forma asincrónica
        await _context.SaveChangesAsync();
    }
}
