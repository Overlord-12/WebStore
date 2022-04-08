using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Mapping;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _Configuration;

    public HomeController(IConfiguration Configuration) => _Configuration = Configuration;

    public IActionResult Index([FromServices] IProductData ProductData)
    {
        var products = ProductData.GetProducts()
           .OrderBy(p => p.Order)
           .Take(6)
           .ToView();

        ViewBag.Products = products;

        return View();
    }

    public IActionResult ContentString(string Id = "-id-")
    {
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