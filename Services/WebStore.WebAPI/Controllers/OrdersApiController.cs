using Microsoft.AspNetCore.Mvc;
using WebStore.DTO;
using WebStore.Interfaces.Services;
using WebStore.Mappers;

namespace WebStore.WebAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersApiController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly ILogger<OrdersApiController> logger;

        public OrdersApiController(IOrderService orderService, ILogger<OrdersApiController> logger)
        {
            this.orderService = orderService;
            this.logger = logger;
        }

        [HttpGet("user/{UserName}")]
        public async Task<IActionResult> GetUserOrdersAsync(string UserName)
        {
            var orders = await orderService.GetUserOrdersAsync(UserName);
            var result = orders.Select(o => OrderMapper.EntityToDTO(o));
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOrderByIdAsync(int Id)
        {
            var order = await orderService.GetOrderByIdAsync(Id);
            if (order is null) return NotFound();

            var result = OrderMapper.EntityToDTO(order);
            return Ok(result);
        }
        [HttpPost("{UserName}")]
        public async Task<IActionResult> CreateOrderAsync(string UserName, [FromBody] CreateOrderDTO model)
        {
            var cart = OrderItemMapper.DTOToCartViewModel(model.Items);
            var orderModel = model.Order;
            var order = await orderService.CreateOrderAsync(UserName, cart, orderModel);

            return CreatedAtAction(nameof(GetOrderByIdAsync), new { order.Id }, OrderMapper.EntityToDTO(order));
        }
    }
}
