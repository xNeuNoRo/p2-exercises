using PracticaAPI.Application.Interfaces.Repositories;
using PracticaAPI.Infrastructure.Repositories;
namespace PracticaAPI.Infrastructure;

/// <summary>
/// Clase estática de extensión para configurar los servicios de infraestructura
/// en el contenedor de dependencias de la aplicación.
/// </summary>
public static class DependencyInjection
{
    /// <remarks>
    /// Extensión para configurar los servicios de infraestructura en el contenedor de dependencias.
    /// </remarks>
    /// <param name="services">El contenedor de servicios de la aplicación.</param>
    /// <param name="configuration">La configuración de la aplicación.</param>
    /// <returns>El contenedor de servicios actualizado.</returns>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // Configuración de rutas para los archivos JSON de tareas y notas
        var basePath = AppContext.BaseDirectory;

        var orderPath = "Data/Orders.json";
        var productPath = "Data/Products.json";

        // Combinamos la ruta base con las rutas relativas para obtener las rutas completas de los archivos JSON
        var orderFilePath = Path.Combine(basePath, orderPath);
        var productFilePath = Path.Combine(basePath, productPath);

        // Registramos el repositorio de órdenes
        services.AddScoped<IOrderRepository>(provider => new OrderRepository(orderFilePath));

        // Registramos el repositorio de productos
        services.AddScoped<IProductRepository>(provider => new ProductRepository(productFilePath));

        // Retornamos el contenedor de servicios actualizado
        return services;
    }
}
