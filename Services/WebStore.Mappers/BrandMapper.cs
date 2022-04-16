using AutoMapper;
using WebStore.Domain.Entities;
using WebStore.DTO;
using WebStore.ViewModels;

namespace WebStore.Mappers
{
    public static class BrandMapper
    {
        public static BrandViewModel DTOToViewModel(BrandDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<BrandDTO, BrandViewModel>());
            var mapper = new Mapper(config);

            var model = mapper.Map<BrandViewModel>(dto);
            return model;
        }
        public static BrandDTO ViewModelToDTO(BrandViewModel model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<BrandViewModel, BrandDTO>());
            var mapper = new Mapper(config);

            var dto = mapper.Map<BrandDTO>(model);
            return dto;
        }

        public static BrandDTO EntityToDTO(Brand brand)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Brand, BrandDTO>());
            var mapper = new Mapper(config);

            var dto = mapper.Map<BrandDTO>(brand);
            return dto;
        }
    }
}
