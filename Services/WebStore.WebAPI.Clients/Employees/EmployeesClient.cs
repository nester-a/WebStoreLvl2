using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using WebStore.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services.DTO;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesDTOData
    {
        private readonly ILogger<EmployeesClient> logger;

        public EmployeesClient(HttpClient client, ILogger<EmployeesClient> logger) : base(client, WebAPIAddresses.Employees)
        {
            this.logger = logger;
        }

        public int Add(EmployeeDTO dto)
        {
            var response = Post(Address, dto);
            var addedEmployee = response.Content.ReadFromJsonAsync<EmployeeDTO>().Result;

            if(addedEmployee is null)
                return -1;

            var id = addedEmployee.Id;
            dto.Id = id;

            return id;
        }

        public bool Delete(int id)
        {
            var response = Delete($"{Address}/{id}");
            var result = response.IsSuccessStatusCode;
            return result;
        }

        public bool Edit(EmployeeDTO dto)
        {
            var response = Put(Address, dto);
            var result = response.EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<bool>()
                .Result;

            return result;
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            var employeesDTO = Get<IEnumerable<EmployeeDTO>>(Address);
            return employeesDTO ?? Enumerable.Empty<EmployeeDTO>();
        }

        public EmployeeDTO? GetById(int id)
        {
            var employeeDTO = Get<EmployeeDTO>($"{Address}/{id}");
            return employeeDTO;
        }
    }
}
