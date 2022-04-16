using AutoMapper;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Orders;
using WebStore.DTO;
using WebStore.ViewModels;

namespace WebStore.Mappers
{
    public class OrderItemMapper
    {
        public static OrderItemViewModel DTOToViewModel(OrderItemDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderItemDTO, OrderItemViewModel>());
            var mapper = new Mapper(config);

            var model = mapper.Map<OrderItemViewModel>(dto);
            return model;
        }
        public static OrderItemDTO ViewModelToDTO(OrderItemViewModel model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderItemViewModel, OrderItemDTO>());
            var mapper = new Mapper(config);

            var dto = mapper.Map<OrderItemDTO>(model);
            return dto;
        }
        public static OrderItemDTO EntityToDTO(OrderItem order)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderItem, OrderItemDTO>()
            .ForMember("ProductID", opt => opt.MapFrom(p => p.Product.Id)));
            var mapper = new Mapper(config);

            var dto = mapper.Map<OrderItemDTO>(order);
            return dto;
        }
        public static OrderItem DTOToEntity(OrderItemDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderItemDTO, OrderItem>()
            .ForMember("Product", opt => opt.MapFrom(p => new Product { Id = dto.Id })));
            var mapper = new Mapper(config);

            var order = mapper.Map<OrderItem>(dto);
            return order;
        }
        public static IEnumerable<OrderItemDTO> CartViewModelToDTO(CartViewModel cart)
        {
            var dto = cart.Items.Select(p => new OrderItemDTO
            {
                ProductId = p.Product.Id,
                Price = p.Product.Price,
                Quantity = p.Quantity,
            });

            return dto;
        }
        public static CartViewModel DTOToCartViewModel(IEnumerable<OrderItemDTO> items)
        {
            return new() { Items = items.Select(p => (new ProductViewModel { Id = p.ProductId }, p.Quantity)) };
        }
    }
}
