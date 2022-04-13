using AutoMapper;
using WebStore.Domain.Entities;
using WebStore.DTO;
using WebStore.ViewModels;

namespace WebStore.Mappers
{
    public class ProductMapper
    {

        public static ProductViewModel DTOToViewModel(ProductDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>());
            var mapper = new Mapper(config);

            var model = mapper.Map<ProductViewModel>(dto);
            return model;
        }
        public static ProductDTO ViewModelToDTO(ProductViewModel model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductViewModel, ProductDTO>());
            var mapper = new Mapper(config);

            var dto = mapper.Map<ProductDTO>(model);
            return dto;
        }

        public static ProductDTO EntityToDTO(Product employee)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>());
            var mapper = new Mapper(config);

            var dto = mapper.Map<EmployeeDTO>(employee);
            return dto;
        }

        public static Product DTOToEntity(ProductDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, Product>());
            var mapper = new Mapper(config);

            var employee = mapper.Map<Product>(dto);
            return employee;
        }
    }
}
