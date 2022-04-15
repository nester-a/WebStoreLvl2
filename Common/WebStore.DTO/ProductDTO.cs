namespace WebStore.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = null!;

        public BrandDTO? Brand { get; set; }

        public SectionDTO Section { get; set; } = null!;
    }
}
