using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleWeb.API.Data;
using SampleWeb.API.Models;

namespace SampleWeb.API.Repositories
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext dbContext;

        public SQLEmployeeRepository(EmployeeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Employee> CreateEmployeeASync(Employee employee)
        {
            await dbContext.Employee.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> DeleteEmployeeAsync(int id)
        {
            var ExistingEmployee = await dbContext.Employee.FindAsync(id);

            if (ExistingEmployee == null)
            {
                return null;
            }

            dbContext.Employee.Remove(ExistingEmployee);
            await dbContext.SaveChangesAsync();
            return ExistingEmployee;
        }

        public async Task<List<Employee>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
                                                       string? sortBy = null, bool isAscending = true)
        {
            var employee = dbContext.Employee.AsQueryable();

            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    employee = employee.Where(x => x.Name.Contains(filterQuery));
                else if (filterOn.Equals("PrimarySkill", StringComparison.OrdinalIgnoreCase))
                    employee = employee.Where(x => x.PrimarySkill.Contains(filterQuery));
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    employee = isAscending ? employee.OrderBy(x => x.Name) : employee.OrderByDescending(x => x.Name);
                else if (sortBy.Equals("Experience", StringComparison.OrdinalIgnoreCase))
                    employee = isAscending ? employee.OrderBy(x => x.Experience) : employee.OrderByDescending(x => x.Experience);
            }

            return await employee.ToListAsync();
            //return await dbContext.Employee.ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIDAsync(int id)
        {
            return await dbContext.Employee.FindAsync(id);
        }

        public async Task<Employee?> UpdateEmployeeAsync(int id, Employee employee)
        {
            var ExistingEmployee = await dbContext.Employee.FindAsync(id);

            if (ExistingEmployee == null) {
                return null;
            }

            ExistingEmployee.Name = employee.Name;
            ExistingEmployee.Experience = employee.Experience;
            ExistingEmployee.PrimarySkill = employee.PrimarySkill;

            await dbContext.SaveChangesAsync();
            return ExistingEmployee;
        }
    }
}
