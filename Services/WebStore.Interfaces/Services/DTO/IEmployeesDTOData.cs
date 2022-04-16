using WebStore.DTO;

namespace WebStore.Interfaces.Services.DTO
{
    public interface IEmployeesDTOData
    {
        IEnumerable<EmployeeDTO> GetAll();

        EmployeeDTO? GetById(int id);

        int Add(EmployeeDTO employee);

        bool Edit(EmployeeDTO employee);

        bool Delete(int id);
    }
}
