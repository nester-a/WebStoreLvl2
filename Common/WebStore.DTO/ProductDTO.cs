namespace WebStore.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = null!;

        public string? Brand { get; set; }

        public string Section { get; set; } = null!;
    }
}
