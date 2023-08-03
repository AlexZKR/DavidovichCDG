using Ardalis.Specification;
using CDG.BLL.Entities.Order;

namespace CDG.BLL.Specifications.OrderSpecifications;
//TODO: make this paged 
public class OrdersByProccessedSpecification : Specification<Order>
{
    public OrdersByProccessedSpecification(bool proccessed)
    {
        Query
            .Where(order => order.IsInProcess == proccessed)
            .Include(o => o.OrderItems);
    }
}