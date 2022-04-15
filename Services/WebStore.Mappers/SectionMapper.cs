using AutoMapper;
using WebStore.DTO;
using WebStore.ViewModels;
using static System.Collections.Specialized.BitVector32;

namespace WebStore.Mappers
{
    public class SectionMapper
    {
        public static SectionViewModel DTOToViewModel(SectionDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SectionDTO, SectionViewModel>());
            var mapper = new Mapper(config);

            var model = mapper.Map<SectionViewModel>(dto);
            return model;
        }
        public static SectionDTO ViewModelToDTO(SectionViewModel model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SectionViewModel, SectionDTO>());
            var mapper = new Mapper(config);

            var dto = mapper.Map<SectionDTO>(model);
            return dto;
        }

        public static SectionDTO EntityToDTO(Section product)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Section, SectionDTO>());
            var mapper = new Mapper(config);

            var dto = mapper.Map<SectionDTO>(product);
            return dto;
        }
    }
}
