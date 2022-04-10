using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

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
                return Ok(employees);

            return NoContent();
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var employee = employeesData.GetById(id);
            if (employee is null)
                return NoContent();

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Employee employee)
        {
            var id = employeesData.Add(employee);
            return CreatedAtAction(nameof(GetById), new { Id = id }, employee);
        }

        [HttpPut]
        public IActionResult Edit(Employee employee)
        {
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
