using Microsoft.AspNetCore.Mvc;
using WebStore.Controllers;
using WebStore.Domain.Entities;
using WebStore.Interface.Interfaces;
using WebStore.ViewModels;

namespace WebStore.WebApi.Controllers
{
    [Route("api/Employee")]
    public class EmployeeController : Controller
    {

        private readonly IEmployeesData _employeesData;

        public EmployeeController(IEmployeesData employeesData)
        {
            _employeesData = employeesData;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var employees = _employeesData.GetAll();
            return Ok(employees);
        }

        [HttpGet("getDetailsAboutEmployee/{id}")]
        public IActionResult GetDetailsAboutEmployee(int id)
        {
            var employee = _employeesData.GetById(id);
            if(employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpGet("editEmployee/{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _employeesData.GetById(id);
            var model = new EmployeesViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Patronymic = employee.Patronymic,
                ShortName = employee.ShortName,
                Age = employee.Age,
            };

            return Ok(model);
        }

        [HttpPut("editEmployee")]
        public IActionResult EditEmployee(Employee Model)
        {
            var employee = new Employee
            {
                Id = Model.Id,
                LastName = Model.LastName,
                FirstName = Model.FirstName,
                Patronymic = Model.Patronymic,
                Age = Model.Age,
            };

            return Ok(_employeesData.Edit(employee));
        }

        [HttpDelete("deleteEmployeeById/{id}")]
        public IActionResult DeleteEmployeeById(int id)
        {
            var employee = _employeesData.GetById(id);
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

            return Ok(model);
        }
    }
}
