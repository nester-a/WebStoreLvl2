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
}
