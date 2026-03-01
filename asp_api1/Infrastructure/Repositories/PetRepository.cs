using App.Domain.Entities;
using App.Domain.Interfaces;
using App.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories;

public class PetRepository : IPetRepository
{
    private readonly AppDbContext _context;

    // Constructor que recibe una instancia de AppDbContext a través de la inyección de dependencias
    public PetRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todas las mascostas del contexto de base de datos.
    /// </summary>
    /// <returns>Una task que representa la operación asincrónica, con un resultado de una colección de objetos Pet.</returns>
    public async Task<IEnumerable<Pet>> GetAllAsync()
    {
        // Utilizamos el método ToListAsync para obtener todas las mascotas de forma asincrónica
        // AsNoTracking se utiliza para mejorar el rendimiento al indicar que no necesitamos
        // realizar un seguimiento de los cambios en las entidades recuperadas
        // Esto es útil para operaciones de solo lectura, como en este caso, donde solo queremos obtener los datos sin modificarlos
        return await _context.Pets.AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Obtiene una mascota por su ID desde el contexto de base de datos.
    /// </summary>
    /// <param name="id">El identificador de la mascota.</param>
    /// <returns>Una task que representa la operación asincrónica, con un resultado del objeto Pet correspondiente o null si no se encuentra.</returns>
    public async Task<Pet?> GetByIdAsync(int id)
    {
        // Utilizamos el método FindAsync para buscar una mascota por su ID de forma asincrónica
        return await _context.Pets.FindAsync(id);
    }

    /// <summary>
    /// Agrega una nueva mascota al contexto de base de datos y guarda los cambios de forma asincrónica.
    /// </summary>
    /// <param name="pet">La mascota a agregar.</param>
    /// <returns>La mascota creada con su ID generado.</returns>
    public async Task<Pet> AddAsync(Pet pet)
    {
        // Agregamos la nueva mascota al contexto de base de datos
        var createdPet = await _context.Pets.AddAsync(pet);

        // Guardamos los cambios en la base de datos de forma asincrónica
        await _context.SaveChangesAsync();

        // Retornamos la mascota creada
        return createdPet.Entity;
    }

    /// <summary>
    ///  Actualiza una mascota existente en el contexto de base de datos y guarda los cambios de forma asincrónica.
    /// </summary>
    /// <param name="pet">La mascota a actualizar.</param>
    /// <returns>Una task que representa la operación asincrónica de actualización.</returns>
    public async Task UpdateAsync(Pet pet)
    {
        // Marcamos la entidad como modificada en el contexto de base de datos
        _context.Pets.Update(pet);

        // Guardamos los cambios en la base de datos de forma asincrónica
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Elimina una mascota por su ID del contexto de base de datos y guarda los cambios de forma asincrónica.
    /// </summary>
    /// <param name="id">
    /// El identificador de la mascota a eliminar.
    /// </param>
    /// <returns>Un valor booleano que indica si la operación de eliminación fue exitosa o no.</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        // Buscamos la mascota por su ID de forma asincrónica
        var pet = await _context.Pets.FindAsync(id);

        // Si la mascota no existe, retornamos false para indicar que no se pudo eliminar porque no se encontró
        if (pet == null)
        {
            return false;
        }

        // Eliminamos la mascota del contexto de base de datos
        _context.Pets.Remove(pet);

        // Guardamos los cambios en la base de datos de forma asincrónica
        await _context.SaveChangesAsync();

        // Retornamos true para indicar que la eliminación fue exitosa
        return true;
    }
}
