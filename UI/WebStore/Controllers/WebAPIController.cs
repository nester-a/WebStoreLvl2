using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.TestAPI;

namespace WebStore.Controllers
{
    public class WebAPIController : Controller
    {
        private readonly IValuesService valuesService;

        public WebAPIController(IValuesService valuesService)
        {
            this.valuesService = valuesService;
        }
        public IActionResult Index()
        {
            var values = valuesService.GetValues();
            return View(values);
        }
    }
}
