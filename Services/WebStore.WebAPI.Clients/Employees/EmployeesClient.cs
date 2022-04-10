using Microsoft.Extensions.Logging;
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
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Edit(Employee employee)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee? GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
