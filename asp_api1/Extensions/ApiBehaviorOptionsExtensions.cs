using App.Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace App.Extensions;

public static class ApiBehaviorOptionsExtensions
{
    // Este metodo de extension se encarga de configurar la respuesta de error personalizada
    // para los casos en que el modelo de datos no sea válido, es decir, cuando el ModelState 
    // no es válido debido a errores de validación en los datos enviados por el cliente.
    public static void ConfigureInvalidModelStateResponse(this ApiBehaviorOptions options)
    {
        // Sobrescribimos la fábrica de respuestas para modelos no válidos,
        // para devolver una respuesta consistente con nuestro formato de ApiResponse
        options.InvalidModelStateResponseFactory = context =>
        {
            // Extraemos los errores de validación del ModelState,
            // creando una lista de objetos con el campo y el mensaje de error
            var errors = context
                .ModelState.Where(e => e.Value?.Errors.Count > 0) // Filtramos solo los campos que tienen errores de validación
                .Select(e => new
                {
                    Field = e.Key, // El nombre del campo que tiene el error de validación
                    Message = e.Value?.Errors[0].ErrorMessage, // Solo mapeamos el primer error de validacion para simplificar la respuesta
                })
                .ToList();

            // Creamos un objeto de respuesta con el formato de ApiResponse,
            // indicando que no fue exitoso y agregando los errores de validación en la sección de error
            var response = new
            {
                Ok = false,
                Data = (object?)null,
                Error = new
                {
                    code = ErrorCodes.ValidationError, // Usamos un código de error específico para errores de validación
                    message = "Uno o más campos tienen errores de validación.",
                    details = errors, // Agregamos los errores de validación en la sección de detalles del error
                },
            };

            // Devolvemos una respuesta HTTP 400 Bad Request con el objeto de respuesta creado
            return new BadRequestObjectResult(response);
        };
    }
}
