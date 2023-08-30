using Microsoft.AspNetCore.Mvc;
using SampleWeb.API.Models;

namespace SampleWeb.API.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync(string? filterOn = null,string? filterQuery = null,
                                           string? sortBy = null, bool isAscending = true);
        Task<Employee?> GetEmployeeByIDAsync(int id);
        Task<Employee> CreateEmployeeASync(Employee employee);
        Task<Employee?> UpdateEmployeeAsync(int id, Employee employee);
        Task<Employee?> DeleteEmployeeAsync(int id);
    }
}
