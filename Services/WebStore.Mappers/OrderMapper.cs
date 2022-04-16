using AutoMapper;
using WebStore.Domain.Entities.Orders;
using WebStore.DTO;
using WebStore.ViewModels;

namespace WebStore.Mappers
{
    public class OrderMapper
    {
        public static OrderViewModel DTOToViewModel(OrderDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, OrderViewModel>());
            var mapper = new Mapper(config);

            var model = mapper.Map<OrderViewModel>(dto);
            return model;
        }
        public static OrderDTO ViewModelToDTO(OrderViewModel model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderViewModel, OrderDTO>());
            var mapper = new Mapper(config);

            var dto = mapper.Map<OrderDTO>(model);
            return dto;
        }
        public static OrderDTO EntityToDTO(Order order)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>()
            .ForMember("Id", opt => opt.MapFrom(o => o.Id)));
            var mapper = new Mapper(config);

            var dto = mapper.Map<OrderDTO>(order);
            return dto;
        }
        public static Order DTOToEntity(OrderDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, Order>());
            var mapper = new Mapper(config);

            var order = mapper.Map<Order>(dto);
            return order;
        }
    }
}
