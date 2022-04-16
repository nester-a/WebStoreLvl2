using System.Net.Http.Json;
using WebStore.Domain.Entities.Orders;
using WebStore.DTO;
using WebStore.Interfaces.Services;
using WebStore.Mappers;
using WebStore.ViewModels;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Orders
{
    public class OrderClient : BaseClient, IOrderService
    {
        public OrderClient(HttpClient client) : base(client, "api/orders")
        {
        }

        public async Task<Order> CreateOrderAsync(string UserName, CartViewModel Cart, OrderViewModel OrderModel, CancellationToken Cancel = default)
        {
            var model = new CreateOrderDTO
            {
                Items = OrderItemMapper.CartViewModelToDTO(Cart),
                Order = OrderModel,
            };
            var response = await PostAsync($"{Address}/{UserName}", model).ConfigureAwait(false);

            var order = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<OrderDTO>(cancellationToken: Cancel)
                .ConfigureAwait(false);

            return OrderMapper.DTOToEntity(order!);
        }

        public async Task<Order?> GetOrderByIdAsync(int Id, CancellationToken Cancel = default)
        {
            var order = await GetAsync<OrderDTO>($"{Address}/{Id}").ConfigureAwait(false);
            return OrderMapper.DTOToEntity(order!);
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(string UserName, CancellationToken Cancel = default)
        {
            var orders = await GetAsync<IEnumerable<OrderDTO>>($"{Address}/user/{UserName}").ConfigureAwait(false);
            var result = orders!.Select(o => OrderMapper.DTOToEntity(o));
            return result;
        }
    }
}
