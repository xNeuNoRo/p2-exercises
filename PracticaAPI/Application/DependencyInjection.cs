using System.Reflection;
using FluentValidation;
using PracticaAPI.Application.Services;

namespace PracticaAPI.Application;

/// <summary>
/// Clase estática de extensión para configurar los servicios de la capa
/// de aplicación en el contenedor de dependencias, permitiendo la inyección
/// de dependencias de los servicios de consulta y comando para tareas y notas,
/// así como la configuración automática de los validadores definidos en el ensamblado
/// de la aplicación, utilizando FluentValidation para la validación de las solicitudes.
/// </summary>
public static class DependencyInjection
{
    /// <remarks>
    /// Extensión para configurar los servicios de la capa de aplicación en el contenedor de dependencias.
    /// </remarks>
    /// <param name="services">El contenedor de servicios de la aplicación</param>
    /// <returns>El contenedor de servicios actualizado.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Escanear el ensamblado actual en busca de clases que implementen AbstractValidator<T>
        // y registrarlas automáticamente (esto es parte de FluentValidation)
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Registrar los servicios de productos y órdenes
        services.AddScoped<OrderService>();
        services.AddScoped<ProductService>();

        // Retornamos el servicio actualizado
        return services;
    }
}
