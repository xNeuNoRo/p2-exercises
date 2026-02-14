using Calculator.Business.Factories;
using Calculator.Data.Repositories;
using Calculator.Entities.Enums;
using Calculator.Entities.Models;

namespace Calculator.Business.Services;

public class CalculatorService
{
    // Dependencias
    private readonly TxtRepo<Operation> _repo;
    private readonly OperationFactory _factory;

    // Inyeccion de dependencias manual
    public CalculatorService(TxtRepo<Operation> repo, OperationFactory factory)
    {
        _repo = repo;
        _factory = factory;
    }

    // Calcula una operacion, la guarda en el repo y la devuelve
    public Operation Calculate(decimal a, decimal b, OperationType type)
    {
        try
        {
            var operation = _factory.Create(a, b, type);
            _repo.Append(operation);
            return operation;
        }
        catch (Exception ex)
        {
            // En caso de error, lanzamos una excepcion con un mensaje mas amigable
            throw new InvalidOperationException($"Error al calcular la operacion: {ex.Message}");
        }
    }

    // Obtiene el historial de operaciones desde el repo
    public List<Operation> GetHistory() => _repo.Load();
}
