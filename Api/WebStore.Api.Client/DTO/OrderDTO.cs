using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Orders;
using WebStore.ViewModels;

namespace WebStore.WebStore.Api.Client.OrderDTO
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? Description { get; set; }

        public DateTimeOffset Date { get; set; }

        public IEnumerable<OrderItemDTO> Items { get; set; } = null!;
    }

    public class OrderItemDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }

    public class CreateOrderDTO
    {
        public OrderViewModel Order { get; set; } = null!;

        public IEnumerable<OrderItemDTO> Items { get; set; } = null!;
    }

    public static class OrderDTOMapper
    {
        public static OrderItemDTO? ToDTO(this OrderItem? Item) => Item is null
            ? null
            : new OrderItemDTO
            {
                Id = Item.Id,
                ProductId = Item.Product.Id,
                Price = Item.Price,
                Quantity = Item.Quantity,
            };

        public static OrderItem? FromDTO(this OrderItemDTO? Item) => Item is null
            ? null
            : new OrderItem
            {
                Id = Item.Id,
                Product = new Product { Id = Item.Id },
                Price = Item.Price,
                Quantity = Item.Quantity,
            };

        public static OrderDTO? ToDTO(this Order? Order) => Order is null
            ? null
            : new()
            {
                Id = Order.Id,
                Address = Order.Address,
                Phone = Order.Phone,
                Date = Order.Date,
                Description = Order.Description,
                Items = Order.Items.Select(ToDTO)!
            };

        public static Order? FromDTO(this OrderDTO? Order) => Order is null
            ? null
            : new()
            {
                Id = Order.Id,
                Address = Order.Address,
                Phone = Order.Phone,
                Date = Order.Date,
                Description = Order.Description,
                Items = Order.Items.Select(FromDTO).ToList()!
            };

        public static IEnumerable<OrderDTO?> ToDTO(this IEnumerable<Order?> Orders) => Orders.Select(ToDTO);
        public static IEnumerable<Order?> FromDTO(this IEnumerable<OrderDTO?> Orders) => Orders.Select(FromDTO);

        public static IEnumerable<OrderItemDTO> ToDTO(this CartViewModel Cart) =>
            Cart.Items.Select(p => new OrderItemDTO
            {
                ProductId = p.Product.Id,
                Price = p.Product.Price,
                Quantity = p.Quantity
            });

        public static CartViewModel ToCartView(this IEnumerable<OrderItemDTO> Items) => new()
        {
            Items = Items.Select(p => (new ProductViewModel { Id = p.ProductId }, p.Quantity))
        };
    }
}
    
