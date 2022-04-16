using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.Services.DTO;
using WebStore.Mappers;
using WebStore.ViewModels;

namespace WebStore.Controllers;

public class CatalogController: Controller
{
    private readonly IProductDTOData _ProductData;

    public CatalogController(IProductDTOData ProductData) => _ProductData = ProductData;

    public IActionResult Index(int? SectionId, int? BrandId)
    {
        var filter = new ProductFilter
        {
            BrandId = BrandId,
            SectionId = SectionId,
        };

        var products = _ProductData.GetProducts(filter);

        return View(new CatalogViewModel
        {
            SectionId = SectionId,
            BrandId = BrandId,
            Products = products
               .OrderBy(p => p.Order)
               .Select(p => ProductMapper.DTOToViewModel(p)),
        });
    }

    public IActionResult Details(int Id)
    {
        var product = _ProductData.GetProductById(Id);

        if (product is null)
            return NotFound();

        return View(ProductMapper.DTOToViewModel(product));
    }
}
