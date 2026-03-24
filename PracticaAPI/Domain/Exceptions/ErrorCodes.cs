namespace PracticaAPI.Domain.Exceptions;

/// <summary>
/// Clase estática que define códigos de error específicos para la aplicación,
/// </summary>
public static class ErrorCodes
{
    public const string ProductNotFound = "PRODUCT_NOT_FOUND";
    public const string OrderNotFound = "ORDER_NOT_FOUND";

    /// <summary>
    /// Código de error para indicar que una solicitud es inválida
    /// </summary>
    public const string BadRequest = "BAD_REQUEST";

    /// <summary>
    /// Código de error para indicar que el acceso no está autorizado
    /// </summary>
    public const string Unauthorized = "UNAUTHORIZED";

    /// <summary>
    /// Código de error para indicar que el acceso está prohibido
    /// </summary>
    public const string Forbidden = "FORBIDDEN";

    /// <summary>
    /// Código de error para indicar que un recurso no fue encontrado
    /// </summary>
    public const string NotFound = "NOT_FOUND";

    /// <summary>
    /// Código de error para indicar un conflicto de recursos
    /// </summary>
    public const string ResourceConflict = "RESOURCE_CONFLICT";

    /// <summary>
    /// Código de error para indicar un error interno del servidor
    /// </summary>
    public const string InternalError = "INTERNAL_SERVER_ERROR";

    /// <summary>
    /// Código de error para indicar un error de validación
    /// </summary>
    public const string ValidationError = "VALIDATION_ERROR";
}
