using System.Reflection;
using App.Application.Services;
using App.Domain.Interfaces;
using FluentValidation;

namespace App.Application;

// Clase de extensión para configurar la inyección de dependencias de la capa de aplicación
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Inyectamos los servicios de la capa de aplicación
        services.AddScoped<IPetService, PetService>();
        services.AddScoped<IProfileService, ProfileService>();

        // Registramos los validadores de FluentValidation que se encuentran en el ensamblado actual
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
