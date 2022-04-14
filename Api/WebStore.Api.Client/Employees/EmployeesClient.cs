using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebStore.Api.Client.Base;
using WebStore.Domain.Entities;
using WebStore.Interface.Interfaces;

namespace WebStore.Api.Client.Employees
{
    internal class EmployeesClient : BaseClient, IEmployeesData
    {
        private readonly ILogger<EmployeesClient> _Logger;

        public EmployeesClient(HttpClient Client, ILogger<EmployeesClient> Logger)
            : base(Client, "api/employees")
        {
            _Logger = Logger;
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = Get<IEnumerable<Employee>>($"_Adress/getAll");
            return employees ?? Enumerable.Empty<Employee>();
        }

        public Employee? GetById(int id)
        {
            var employee = Get<Employee>($"{_Adress}/getDetailsAboutEmployee/{id}");
            return employee;
        }

        public int Add(Employee employee)
        {
            var response = Post(_Adress, employee);
            var added_employee = response.Content.ReadFromJsonAsync<Employee>().Result;
            if (added_employee is null)
                return -1;

            var id = added_employee.Id;
            employee.Id = id;
            return id;
        }

        public bool Edit(Employee employee)
        {
            var response = Put("_Adress/editEmployee", employee);

            var success = response.EnsureSuccessStatusCode()
               .Content
               .ReadFromJsonAsync<bool>()
               .Result;

            return success;
        }

        public bool Delete(int Id)
        {
            var response = Delete($"{_Adress}/deleteEmployeeById/{Id}");
            var success = response.IsSuccessStatusCode;
            return success;
        }
    }
}
