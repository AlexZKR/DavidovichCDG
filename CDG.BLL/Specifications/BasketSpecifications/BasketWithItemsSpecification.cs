#pragma warning disable CS0618 // Disable Ardalis obsolete warning


using Ardalis.Specification;
using CDG.BLL.Entities.BasketAggregate;

namespace CDG.BLL.Specifications;

public sealed class BasketWithItemsSpecification : Specification<Basket>, ISingleResultSpecification
{
    public BasketWithItemsSpecification(int basketId)
    {
        Query
            .Where(b => b.Id == basketId)
            .Include(b => b.Items);
    }

    public BasketWithItemsSpecification(string username)
    {
        Query
            .Where(b => b.BuyerId == username)
            .Include(b => b.Items);
    }
}
