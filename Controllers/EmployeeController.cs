using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleWeb.API.CustomActionFilters;
using SampleWeb.API.Data;
using SampleWeb.API.Models;
using SampleWeb.API.Models.DTO;
using SampleWeb.API.Repositories;

namespace SampleWeb.API.Controllers
{
    // https://localhost:7110/api/employee
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        // GET All Employee Data 
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending = true)
        {
            // Getting Data from Db using Domain models
            var employees = await employeeRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true);
            return Ok(mapper.Map<List<EmployeeDto>>(employees));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await employeeRepository.GetEmployeeByIDAsync(id);

            if (employee == null) { return NotFound($"Employee with Id = {id} not found"); }

            return Ok(mapper.Map<EmployeeDto>(employee));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await employeeRepository.DeleteEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound($"Employee with Id = {id} not found");
            }
            return Ok(mapper.Map<EmployeeDto>(employee));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            var employee = mapper.Map<Employee>(createEmployeeDto);
            employee = await employeeRepository.CreateEmployeeASync(employee);

            var employeeDto = mapper.Map<EmployeeDto>(employee);

            return CreatedAtAction(nameof(GetEmployee), new { Id = employeeDto.Id }, employeeDto);

        }

        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            var employeeModel = mapper.Map<Employee>(updateEmployeeDto);
            var employee = await employeeRepository.UpdateEmployeeAsync(id, employeeModel);

            if (employee == null)
                return NotFound("Emp with Id " + id + " Not found");

            return Ok(mapper.Map<UpdateEmployeeDto>(employee));
        }
    }
}
