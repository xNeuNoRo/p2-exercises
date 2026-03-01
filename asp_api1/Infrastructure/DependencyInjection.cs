using App.Domain.Interfaces;
using App.Infrastructure.Data;
using App.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // Configuramos el contexto de la base de datos utilizando SQL Server
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"), // Obtenemos la cadena de conexión desde el archivo de configuración (appsettings.json)
                sqlServerOptionsAction =>
                {
                    // Configuramos el tiempo de espera para las operaciones de la base de datos a 30 segundos
                    sqlServerOptionsAction.CommandTimeout(30);

                    // Habilitamos la detección automática de la versión del servidor SQL
                    // para asegurar la compatibilidad con diferentes versiones
                    sqlServerOptionsAction.EnableRetryOnFailure();
                }
            )
        );

        // Inyectamos los repositorios de la capa de infraestructura
        // para que puedan ser utilizados en los servicios de la capa de aplicación
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IProfileRepository, ProfileRepository>();

        return services;
    }
}
