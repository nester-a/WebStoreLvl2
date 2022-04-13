using AutoMapper;
using WebStore.Domain.Entities;
using WebStore.DTO;
using WebStore.ViewModels;

namespace WebStore.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeesViewModel DTOToViewModel(EmployeeDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, EmployeesViewModel>());
            var mapper = new Mapper(config);

            var model = mapper.Map<EmployeesViewModel>(dto);
            return model;
        }
        public static EmployeeDTO ViewModelToDTO(EmployeesViewModel model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeesViewModel, EmployeeDTO>());
            var mapper = new Mapper(config);

            var dto = mapper.Map<EmployeeDTO>(model);
            return dto;
        }

        public static EmployeeDTO EntityToDTO(Employee employee)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>());
            var mapper = new Mapper(config);

            var dto = mapper.Map<EmployeeDTO>(employee);
            return dto;
        }

        public static Employee DTOToEntity(EmployeeDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, Employee>());
            var mapper = new Mapper(config);

            var employee = mapper.Map<Employee>(dto);
            return employee;
        }
    }
}
