using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.APIAdresses;
using WebStore.Interface.Interfaces;
using WebStore.ViewModels;

namespace WebStore.WebApi.Controllers
{
    [Route(WebAPIAdresses.V1.Orders)]
    public class OrderController : Controller
    {
        public IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;   
        }

        [HttpGet("getUserOrders/{name}")]
        public async Task<IActionResult> GetUserOrders(string name)
        {
            var orders = await _orderService.GetUserOrdersAsync(name);

            return Ok(orders);
        }

        [HttpGet("getOrderById/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            return Ok(order);
        }

        public async Task<IActionResult> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel)
        {
            var result = await _orderService.CreateOrderAsync(UserName, Cart, OrderModel);

            return Ok(result);
        }

    }
}
