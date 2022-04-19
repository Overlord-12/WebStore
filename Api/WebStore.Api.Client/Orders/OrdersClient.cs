using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Api.Client.Base;
using WebStore.Domain.APIAdresses;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Orders;
using WebStore.Interface.Interfaces;
using WebStore.ViewModels;
using WebStore.WebStore.Api.Client.OrderDTO;

namespace WebStore.Api.Client.Orders
{
    internal class OrdersClient : BaseClient, IOrderService
    {
        private readonly ILogger<OrdersClient> _Logger;

        public OrdersClient(HttpClient Client, ILogger<OrdersClient> Logger)
            : base(Client, WebAPIAdresses.V1.Orders)
        {
            _Logger = Logger;
        }

        public async Task<Order> CreateOrderAsync(string UserName, CartViewModel Cart, OrderViewModel OrderModel, CancellationToken Cancel = default)
        {
            var result = await CreateOrderAsync(UserName, Cart, OrderModel, Cancel).ConfigureAwait(false);

            return result;
        }

        public async Task<Order?> GetOrderByIdAsync(int Id, CancellationToken Cancel = default)
        {
            var order = await GetAsync<Order>($"{_Address}/getOrderById/{Id}");

            return order;
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(string UserName, CancellationToken Cancel = default)
        {
            var orders = await GetAsync<IEnumerable<OrderDTO>>($"{_Address}/getUserOrders/{UserName}").ConfigureAwait(false);

            return orders!.FromDTO()!;
        }
    }
}
