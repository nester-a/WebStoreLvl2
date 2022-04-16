using WebStore.ViewModels;

namespace WebStore.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Phone { get; set; } = null;
        public string Address { get; set; } = null;
        public string? Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public IEnumerable<OrderItemDTO> Items { get; set; } = null;
    }
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
    public class CreateOrderDTO
    {
        public OrderViewModel Order { get; set; } = null;

        public IEnumerable<OrderItemDTO> Items { get; set; } = null;
    }
}
