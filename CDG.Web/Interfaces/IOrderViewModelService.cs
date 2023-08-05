
using CDG.Web.Models.Order;

namespace CDG.Web.Services;

public interface IOrderViewModelService
{
    Task<OrderViewModel> CreateOrderViewModelAsync(int orderId);
}