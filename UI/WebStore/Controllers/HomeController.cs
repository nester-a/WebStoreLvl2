using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.Services.DTO;
using WebStore.Mappers;

namespace WebStore.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _Configuration;

    public HomeController(IConfiguration Configuration) => _Configuration = Configuration;

    public IActionResult Index([FromServices] IProductDTOData ProductData)
    {
        var products = ProductData.GetProducts()
           .OrderBy(p => p.Order)
           .Take(6)
           .Select(p => ProductMapper.DTOToViewModel(p));


        ViewBag.Products = products;

        return View();
    }

    public IActionResult ContentString(string Id = "-id-")
    {
        if(Id is null) throw new ArgumentNullException(nameof(Id));
        return Content($"content: {Id}");
    }

    public IActionResult ConfigStr()
    {
        return Content($"config: {_Configuration["ServerGreetings"]}");
    }

    public IActionResult Sum(int a, int b)
    {
        return Content((a + b).ToString());
    }
}