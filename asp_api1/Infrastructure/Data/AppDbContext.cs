using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data;

public class AppDbContext : DbContext
{
    // Constructor que recibe las opciones de configuración para la base de datos
    // y las pasa a la clase base DbContext
    public AppDbContext(DbContextOptions options)
        : base(options) { }

    // Propiedades que representan las tablas de la base de datos para los perfiles y las mascotas
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Pet> Pets { get; set; }

    // Método que se ejecuta al crear el modelo de la base de datos
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Llamamos al método base para asegurarnos de que
        // se apliquen las configuraciones predeterminadas
        base.OnModelCreating(modelBuilder);

        // seedeamos la data para la tabla de perfiles
        // utilizando el método HasData
        modelBuilder
            .Entity<Profile>()
            .HasData(
                // Aqui con los datos mios xd
                new Profile
                {
                    Id = 1,
                    Name = "Angel Gonzalez Muñoz",
                    Career = "Desarrollo de Software",
                    StudentId = "2025-1122",
                }
            );
    }
}
