using App.Domain.Entities;
using App.Infrastructure.Repositories;

namespace App.Services;

public class EmployeeService
{
    private readonly EmployeeRepository _repo;

    public EmployeeService(EmployeeRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        return await _repo.GetAllAsync();
    }

    public async Task AddEmployee(Employee employee)
    {
        await _repo.AddEmployee(employee);
    }
}