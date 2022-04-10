using Microsoft.AspNetCore.Mvc;
using WebStore.Interface.Interfaces;

namespace WebStore.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestClientService _clientService;
        public TestController(ITestClientService clientService)
        {
            _clientService = clientService;
        }
        public IActionResult Index()
        {
            var response = _clientService.GetString("1");
            return View(response);
        }
    }
}
