using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.DTO;
using WebStore.Interfaces.Services;
using WebStore.Mappers;

namespace WebStore.WebAPI.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesApiController : ControllerBase
    {
        private readonly IEmployeesData employeesData;
        private readonly ILogger<EmployeesApiController> logger;

        public EmployeesApiController(IEmployeesData employeesData, ILogger<EmployeesApiController> logger)
        {
            this.employeesData = employeesData;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = employeesData.GetAll();

            if (employees.Any())
            {
                var dtoEmployees = employees.Select(x => EmployeeMapper.EntityToDTO(x)).ToList();
                return Ok(dtoEmployees);
            }

            return NoContent();
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var employee = employeesData.GetById(id);
            if (employee is null)
                return NoContent();

            var dto = EmployeeMapper.EntityToDTO(employee);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Add([FromBody] EmployeeDTO dto)
        {
            var employee = EmployeeMapper.DTOToEntity(dto);
            var id = employeesData.Add(employee);
            return CreatedAtAction(nameof(GetById), new { Id = id }, EmployeeMapper.EntityToDTO(employee));
        }

        [HttpPut]
        public IActionResult Edit(EmployeeDTO dto)
        {
            var employee = EmployeeMapper.DTOToEntity(dto);
            var result = employeesData.Edit(employee);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = employeesData.Delete(id);
            return result
                ? Ok(true)
                : NotFound(false);
        }
    }
}
