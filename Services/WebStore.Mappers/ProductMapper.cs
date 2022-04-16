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
        public static ProductDTO EntityToDTO(Product product)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()
            .ForMember("Brand", opt => opt.MapFrom(p => BrandMapper.EntityToDTO(p.Brand!)))
            .ForMember("Section", opt => opt.MapFrom(p => SectionMapper.EntityToDTO(p.Section)))
            );
            var mapper = new Mapper(config);

            var dto = mapper.Map<ProductDTO>(product);
            return dto;
        }
        public static Product DTOToEntity(ProductDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, Product>());
            var mapper = new Mapper(config);

            var employee = mapper.Map<Product>(dto);
            return employee;
        }

        public static ProductViewModel EntityToViewModel(Product product)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductViewModel>()

            .ForMember("Section", opt => opt.MapFrom(p => p.Section.Name))
            .ForMember("Brand", opt => opt.MapFrom(p => p.Brand.Name)));
            var mapper = new Mapper(config);

            var model = mapper.Map<ProductViewModel>(product);
            return model;
        }
        //public static Product ViewModelToEntity(ProductViewModel model)
        //{
        //    var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductViewModel, Product>()
        //    .ForMember("Brand", opt => opt.MapFrom())
        //    .ForMember("Section", opt => opt.MapFrom()));
        //    var mapper = new Mapper(config);
        //    var entity = mapper.Map<Product>(model);
        //    return entity;
        //}
    }
}
