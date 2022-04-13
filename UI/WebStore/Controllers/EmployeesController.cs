using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.Services;
using WebStore.Mappers;
using WebStore.ViewModels;

namespace WebStore.Controllers;

//[Route("Staff/{action=Index}/{id?}")]
[Authorize]
public class EmployeesController : Controller
{
    private readonly IEmployeesDTOData _EmployeesData;
    private readonly ILogger<EmployeesController> _Logger;

    public EmployeesController(IEmployeesDTOData EmployeesData, ILogger<EmployeesController> Logger)
    {
        _EmployeesData = EmployeesData;
        _Logger = Logger;
    }

    public IActionResult Index()
    {
        var employeesDTO = _EmployeesData.GetAll();
        var employees = employeesDTO.Select(x => EmployeeMapper.DTOToViewModel(x)).ToList();
        return View(employees);
    }

    //[Route("~/employees/info({Id:int})")]
    public IActionResult Details(int Id)
    {
        var employeeDTO = _EmployeesData.GetById(Id);

        if (employeeDTO == null)
            return NotFound();

        var employee = EmployeeMapper.DTOToViewModel(employeeDTO);
        return View(employee);
    }

    [Authorize(Roles = Role.Adinistrators)]
    public IActionResult Create()
    {
        return View("Edit", new EmployeesViewModel());
    }

    [Authorize(Roles = Role.Adinistrators)]
    public IActionResult Edit(int? Id)
    {
        if (Id is not { } id)
            return View(new EmployeesViewModel());

        var employeeDTO = _EmployeesData.GetById(id);
        if (employeeDTO is null)
            return NotFound();

        var model = EmployeeMapper.DTOToViewModel(employeeDTO);

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = Role.Adinistrators)]
    public IActionResult Edit(EmployeesViewModel Model)
    {
        if (Model.LastName == "Иванов" && Model.Age < 21)
            ModelState.AddModelError("", "Иванов должен быть старше 21 года");

        if (!ModelState.IsValid) return View(Model);

        var employeeDTO = EmployeeMapper.ViewModelToDTO(Model);

        if (Model.Id == 0)
        {
            var id = _EmployeesData.Add(employeeDTO);
            return RedirectToAction(nameof(Details), new { id });
        }

        _EmployeesData.Edit(employeeDTO);

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = Role.Adinistrators)]
    public IActionResult Delete(int id)
    {
        if (id <= 0)
            return BadRequest();

        var employeeDTO = _EmployeesData.GetById(id);
        if (employeeDTO is null)
            return NotFound();

        var model = EmployeeMapper.DTOToViewModel(employeeDTO);

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = Role.Adinistrators)]
    public IActionResult DeleteConfirmed(int Id)
    {
        if (!_EmployeesData.Delete(Id))
            return NotFound();

        return RedirectToAction(nameof(Index));
    }
}