using CDG.BLL.Entities.Enums;
using CDG.BLL.Entities.Order;
using CDG.BLL.Exceptions;
using CDG.BLL.Interfaces;
using CDG.BLL.Specifications.OrderSpecifications;
using CDG.Web.Infrastructure;
using CDG.Web.Models.Order;

namespace CDG.Web.Services;

public class OrderViewModelService : IOrderViewModelService
{
    private readonly IRepository<Order> orderRepository;

    public OrderViewModelService(IRepository<Order> orderRepository)
    {
        this.orderRepository = orderRepository;
    }
    public async Task<OrderViewModel> CreateOrderViewModelAsync(int orderId)
    {
        var order = await GetOrderWithItemsAsync(orderId);
        var vm = MapOrderToViewModel(order);
        return vm;
    }


    //private helpers
    private async Task<Order> GetOrderWithItemsAsync(int orderId)
    {
        var spec = new OrderWithItemsByIdSpecification(orderId);
        var order = await orderRepository.FirstOrDefaultAsync(spec);
        if (order == null) throw new NotFoundException($"Order with id {orderId} not found in db");
        return order;
    }

    private OrderViewModel MapOrderToViewModel(Order order)
    {
        return new OrderViewModel
        {
            IsInProcess = order.IsInProcess,

            OrderId = order.Id,
            BuyerId = order.Buyer.BuyerId,
            TotalPrice = order.TotalPrice,
            FullPrice = order.FullPrice,
            TotalDiscount = order.TotalDiscount,
            BuyerFirstName = order.Buyer.FirstName,
            BuyerLastName = order.Buyer.LastName,
            Email = order.Buyer.Email,
            PhoneNumber = order.Buyer.PhoneNumber,
            OrderDate = order.OrderInfo.OrderDate.ToString("dd.MM.yyyy"),
            Units = order.TotalItems,

            Items = order.OrderItems.Select(i => new OrderItemViewModel
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                FullPrice = i.TotalPrice,
                DiscountedPrice = i.DiscountedPrice,
                Discount = i.Discount,
                Units = i.Units,
                AddInfo = i.AddInfo
            }).ToList(),
        };
    }
}