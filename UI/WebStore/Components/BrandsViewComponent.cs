using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.Services.DTO;
using WebStore.ViewModels;

namespace WebStore.Components;

public class BrandsViewComponent : ViewComponent
{

    private readonly IProductDTOData _ProductData;

    public BrandsViewComponent(IProductDTOData ProductData) => _ProductData = ProductData;

    public IViewComponentResult Invoke() => View(GetBrands());

    private IEnumerable<BrandViewModel> GetBrands() =>
        _ProductData.GetBrands()
           .OrderBy(b => b.Order)
           .Select(b => new BrandViewModel
            {
               Id = b.Id,
               Name = b.Name,
            });
}
