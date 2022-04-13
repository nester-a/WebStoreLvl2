using WebStore.Domain;
using WebStore.DTO;

namespace WebStore.Interfaces.Services.DTO
{
    public interface IProductDTOData
    {
        IEnumerable<SectionDTO> GetSections();

        SectionDTO? GetSectionById(int Id);

        IEnumerable<BrandDTO> GetBrands();

        BrandDTO? GetBrandById(int Id);

        IEnumerable<ProductDTO> GetProducts(ProductFilter? Filer = null);

        ProductDTO? GetProductById(int Id);
    }
}
