using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Interfaces.Services;
using WebStore.Mappers;

namespace WebStore.WebAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductData products;
        private readonly ILogger<ProductsApiController> logger;

        public ProductsApiController(IProductData products, ILogger<ProductsApiController> logger)
        {
            this.products = products;
            this.logger = logger;
        }

        [HttpGet("{Id}")]
        public IActionResult GetBrandById(int Id)
        {
            var brand = products.GetBrandById(Id);
            if (brand is null)
                return NoContent();

            var dto = BrandMapper.EntityToDTO(brand);
            return Ok(dto);
        }
        [HttpPost()]
        public IActionResult GetProducts([FromBody]ProductFilter? Filer = null)
        {
            var entitys = products.GetProducts(Filer);
            var result = entitys.Select(p => ProductMapper.EntityToDTO(p));
            return Ok(result);
        }
    }
}
