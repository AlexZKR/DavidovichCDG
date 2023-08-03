using Ardalis.Specification;
using CDG.BLL.Entities.Order;

namespace CDG.BLL.Specifications.OrderSpecifications;

public class OrderWithItemsByIdSpecification : Specification<Order>
{
    public OrderWithItemsByIdSpecification(int orderId)
    {
        Query
            .Where(order => order.Id == orderId)
            .Include(o => o.OrderItems);
    }
}