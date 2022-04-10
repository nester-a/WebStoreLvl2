using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Employees
{
    internal class EmployeesClient : BaseClient, IEmployeesData
    {
        private readonly ILogger<EmployeesClient> logger;

        public EmployeesClient(HttpClient client, ILogger<EmployeesClient> logger) : base(client, "api/employees")
        {
            this.logger = logger;
        }

        public int Add(Employee employee)
        {
            var response = Post(Address, employee);
            var addedEmployee = response.Content.ReadFromJsonAsync<Employee>().Result;

            if(addedEmployee is null)
                return -1;

            var id = addedEmployee.Id;
            employee.Id = id;

            return id;
        }

        public bool Delete(int id)
        {
            var response = Delete($"{Address}/{id}");
            var result = response.IsSuccessStatusCode;
            return result;
        }

        public bool Edit(Employee employee)
        {
            var response = Put(Address, employee);
            var result = response.EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<bool>()
                .Result;

            return result;
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = Get<IEnumerable<Employee>>(Address);
            return employees ?? Enumerable.Empty<Employee>();
        }

        public Employee? GetById(int id)
        {
            var employee = Get<Employee>($"{Address}/{id}");
            return employee;
        }
    }
}
