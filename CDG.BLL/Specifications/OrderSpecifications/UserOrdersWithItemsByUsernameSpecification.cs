using Ardalis.Specification;
using CDG.BLL.Entities.Order;

namespace CDG.BLL.Specifications.OrderSpecifications;

public class UserOrdersWithItemsByUsernameSpecification : Specification<Order>
{
    public UserOrdersWithItemsByUsernameSpecification(string username)
    {
        Query
            .Where(order => order.Buyer.BuyerId == username)
            .Include(o => o.OrderItems);
    }
}