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

        [HttpGet("brands/{Id}")]
        public IActionResult GetBrandById(int Id)
        {
            var brand = products.GetBrandById(Id);
            if (brand is null)
                return NoContent();

            var dto = BrandMapper.EntityToDTO(brand);
            return Ok(dto);
        }

        [HttpGet("brands")]
        public IActionResult GetBrands()
        {
            var brands = products.GetBrands();
            if (brands is null)
                return NoContent();

            var dtoBrands = brands.Select(b => BrandMapper.EntityToDTO(b));
            return Ok(dtoBrands);
        }

        [HttpGet("{Id}")]
        public IActionResult GetProductById(int Id)
        {
            var product = products.GetProductById(Id);
            if (product is null)
                return NoContent();
            var dto = ProductMapper.EntityToDTO(product);
            return Ok(dto);
        }

        [HttpPost()]
        public IActionResult GetProducts([FromBody]ProductFilter? Filer = null)
        {
            var entitys = products.GetProducts(Filer);
            var result = entitys.Select(p => ProductMapper.EntityToDTO(p));
            return Ok(result);
        }

        [HttpGet("sections/{Id}")]
        public IActionResult GetSectionById(int Id)
        {
            var section = products.GetSectionById(Id);
            if (section is null)
                return NoContent();

            var dto = SectionMapper.EntityToDTO(section);
            return Ok(dto);
        }

        [HttpGet("sections")]
        public IActionResult GetSections()
        {
            var sections = products.GetSections();
            if (sections is null)
                return NoContent();

            var dtoSections = sections.Select(s => SectionMapper.EntityToDTO(s));
            return Ok(dtoSections);
        }
    }
}
