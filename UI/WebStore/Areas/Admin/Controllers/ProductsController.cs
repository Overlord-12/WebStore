using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;
using WebStore.Interface.Interfaces;

namespace WebStore.Areas.Admin.Controllers;

//[/*Area("Admin"), */Authorize(Roles = Role.Adinistrators)]
[Authorize(Policy = "AdminAuthorizationPolicy")]
public class ProductsController : Controller
{
    private readonly IProductData _ProductData;
    private readonly ILogger<ProductsController> _Logger;

    public ProductsController(IProductData ProductData, ILogger<ProductsController> Logger)
    {
        _ProductData = ProductData;
        _Logger = Logger;
    }

    public IActionResult Index()
    {
        var products = _ProductData.GetProducts();
        return View(products);
    }
}
