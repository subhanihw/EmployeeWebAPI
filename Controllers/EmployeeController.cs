using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleWeb.API.Data;
using SampleWeb.API.Models;
using SampleWeb.API.Models.DTO;

namespace SampleWeb.API.Controllers
{
    // https://localhost:7110/api/employee
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _dbContext;

        public EmployeeController(EmployeeDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        // GET: https://localhost:7110/api/employee
        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            // Getting Data from Db using Domain models
            var employees = _dbContext.Employee.ToList();

            // Map Domain Models to DTOs
            var employeeDto = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                employeeDto.Add(new EmployeeDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    PrimarySkill = employee.PrimarySkill,
                    Experience = employee.Experience
                });
            }
            return Ok(employeeDto);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _dbContext.Employee.Find(id);
            if (employee == null) { return NotFound($"Employee with Id = {id} not found"); }

            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                PrimarySkill = employee.PrimarySkill,
                Experience = employee.Experience
            };

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _dbContext.Employee.Find(id);
            if (employee == null)
            {
                return NotFound($"Employee with Id = {id} not found");
            }
            _dbContext.Employee.Remove(employee);
            _dbContext.SaveChanges();

            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                PrimarySkill = employee.PrimarySkill,
                Experience = employee.Experience
            };
            return Ok(employeeDto);

        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            var employee = new Employee
            {
                Name = createEmployeeDto.Name,
                PrimarySkill = createEmployeeDto.PrimarySkill,
                Experience = createEmployeeDto.Experience
            };

            _dbContext.Employee.Add(employee);
            _dbContext.SaveChanges();


            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                PrimarySkill = employee.PrimarySkill,
                Experience = employee.Experience
            };
            return CreatedAtAction(nameof(GetEmployee), new { Id = employeeDto.Id }, employeeDto);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = _dbContext.Employee.Find(id);

            if (employee == null)
                return NotFound("Emp with Id " + id + "Not found");

            employee.Name = updateEmployeeDto.Name;
            employee.PrimarySkill = updateEmployeeDto.PrimarySkill;
            employee.Experience = updateEmployeeDto.Experience;

            _dbContext.SaveChanges();

            var employeeDto = new UpdateEmployeeDto
            {
                Name = employee.Name,
                PrimarySkill = employee.PrimarySkill,
                Experience = employee.Experience
            };
            return Ok(employeeDto);

        }
    }
}
