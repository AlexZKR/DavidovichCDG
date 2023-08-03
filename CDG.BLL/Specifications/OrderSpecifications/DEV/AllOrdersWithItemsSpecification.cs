using Ardalis.Specification;
using CDG.BLL.Entities.Order;

namespace CDG.BLL.Specifications.OrderSpecifications;
//TODO: make this paged
public class AllOrdersWithItemsSpecification : Specification<Order>
{
    public AllOrdersWithItemsSpecification()
    {
        Query
            .Include(o => o.OrderItems);
    }
}