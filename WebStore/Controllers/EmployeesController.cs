using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers;

//[Route("Staff/{action=Index}/{id?}")]
[Authorize]
public class EmployeesController : Controller
{
    private readonly IEmployeesData _EmployeesData;
    private readonly ILogger<EmployeesController> _Logger;

    public EmployeesController(IEmployeesData EmployeesData, ILogger<EmployeesController> Logger)
    {
        _EmployeesData = EmployeesData;
        _Logger = Logger;
    }

    public IActionResult Index()
    {
        var employees = _EmployeesData.GetAll();
        return View(employees);
    }

    //[Route("~/employees/info({Id:int})")]
    public IActionResult Details(int Id)
    {
        var employee = _EmployeesData.GetById(Id);

        if(employee == null)
            return NotFound();

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

        var employee = _EmployeesData.GetById(id);
        if (employee is null)
            return NotFound();

        var model = new EmployeesViewModel
        {
            Id = employee.Id,
            LastName = employee.LastName,
            FirstName = employee.FirstName,
            Patronymic = employee.Patronymic,
            ShortName = employee.ShortName,
            Age = employee.Age,
        };

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = Role.Adinistrators)]
    public IActionResult Edit(EmployeesViewModel Model)
    {
        if (Model.LastName == "Иванов" && Model.Age < 21)
            ModelState.AddModelError("", "Иванов должен быть старше 21 года");

        if (!ModelState.IsValid) return View(Model);

        var employee = new Employee
        {
            Id = Model.Id,
            LastName = Model.LastName,
            FirstName = Model.FirstName,
            Patronymic = Model.Patronymic,
            Age = Model.Age,
        };

        if (Model.Id == 0)
        {
            var id = _EmployeesData.Add(employee);
            return RedirectToAction(nameof(Details), new { id });
        }

        _EmployeesData.Edit(employee);

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = Role.Adinistrators)]
    public IActionResult Delete(int id)
    {
        if (id <= 0)
            return BadRequest();

        var employee = _EmployeesData.GetById(id);
        if (employee is null)
            return NotFound();

        var model = new EmployeesViewModel
        {
            Id = employee.Id,
            LastName = employee.LastName,
            FirstName = employee.FirstName,
            Patronymic = employee.Patronymic,
            ShortName = employee.ShortName,
            Age = employee.Age,
        };

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