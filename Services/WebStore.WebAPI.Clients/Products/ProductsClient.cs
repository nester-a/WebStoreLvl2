using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using WebStore.Domain;
using WebStore.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services.DTO;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Products
{
    public class ProductsClient : BaseClient, IProductDTOData
    {
        private readonly ILogger<ProductsClient> logger;

        public ProductsClient(HttpClient client, ILogger<ProductsClient> logger) : base(client, WebAPIAddresses.V1.Products)
        {
            this.logger = logger;
        }

        public BrandDTO? GetBrandById(int Id)
        {
            var dto = Get<BrandDTO>($"{Address}/brands/{Id}");
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

        public IEnumerable<ProductDTO> GetProducts(ProductFilter? Filer = null)
        {
            var dto = Post(Address, Filer ?? new());
            var result = dto.EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<IEnumerable<ProductDTO>>()
                .Result;

            return result!;
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
