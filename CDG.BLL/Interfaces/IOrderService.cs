using CDG.BLL.Entities.Order;

namespace CDG.BLL.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Buyer buyer, OrderInfo orderInfo);
    Task<List<Order>> GetBuyersOrdersAsync(string username);

    // api operations
    //TODO: make this paged, bool proccessed
    Task<List<Order>> GetAllOrdersAsync();
    Task<List<Order>> GetAllBuyersAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task<Order> GetOrderByUsernameAsync(string username);
    Task<Order> ApproveOrderByIdAsync(int id);

}