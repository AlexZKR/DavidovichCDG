using Ardalis.Specification;
using CDG.BLL.Entities.Order;

namespace CDG.BLL.Specifications.OrderSpecifications;
//TODO: make this paged
public class BuyersOnlySpecification : Specification<Order>
{
    public BuyersOnlySpecification()
    {
        Query.Include(o => o.Buyer);
    }
}