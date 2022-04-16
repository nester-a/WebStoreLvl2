using WebStore.Domain.Entities.Orders;
using WebStore.Interfaces.Services;
using WebStore.ViewModels;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Orders
{
    public class OrderClient : BaseClient, IOrderService
    {
        public OrderClient(HttpClient client) : base(client, "api/orders")
        {
        }

        public Task<Order> CreateOrderAsync(string UserName, CartViewModel Cart, OrderViewModel OrderModel, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<Order?> GetOrderByIdAsync(int Id, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetUserOrdersAsync(string UserName, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}
