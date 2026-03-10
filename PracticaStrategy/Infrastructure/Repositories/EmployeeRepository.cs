using App.Domain.Entities;
using App.Infrastructure.Repositories.Base;

namespace App.Infrastructure.Repositories;

public class EmployeeRepository : JsonBaseRepo<Employee>
{
    public EmployeeRepository(string filePath)
        : base(filePath) { }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        var employees = await base.LoadAsync();
        return employees;
    }

    public async Task AddEmployee(Employee employee)
    {
        await AppendAsync(employee);
    }
}
