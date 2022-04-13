using Microsoft.Extensions.Logging;
using WebStore.Domain;
using WebStore.DTO;
using WebStore.Interfaces.Services.DTO;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Products
{
    public class ProductsClient : BaseClient, IProductDTOData
    {
        private readonly ILogger<ProductsClient> logger;

        public ProductsClient(HttpClient client, ILogger<ProductsClient> logger) : base(client, "api/products")
        {
            this.logger = logger;
        }

        public BrandDTO? GetBrandById(int Id)
        {
            var dto = Get<BrandDTO>($"{Address}/{Id}");
            return dto;
        }

        public IEnumerable<BrandDTO> GetBrands()
        {
            var dto = Get<IEnumerable<BrandDTO>>(Address);
            return dto ?? Enumerable.Empty<BrandDTO>();
        }

        public ProductDTO? GetProductById(int Id)
        {
            var dto = Get<ProductDTO>($"{Address}/{Id}");
            return dto;
        }

        //ДОБАВИТЬ ФИЛЬТРАЦИЮ
        public IEnumerable<ProductDTO> GetProducts(ProductFilter? Filer = null)
        {
            var dto = Get<IEnumerable<ProductDTO>>(Address);
            return dto ?? Enumerable.Empty<ProductDTO>();
        }

        public SectionDTO? GetSectionById(int Id)
        {
            var dto = Get<SectionDTO>($"{Address}/{Id}");
            return dto;
        }

        public IEnumerable<SectionDTO> GetSections()
        {
            var dto = Get<IEnumerable<SectionDTO>>(Address);
            return dto ?? Enumerable.Empty<SectionDTO>();
        }
    }
}
