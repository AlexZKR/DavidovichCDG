#pragma warning disable CS0618 // Disable Ardalis obsolete warning


using Ardalis.Specification;
using CDG.BLL.Entities.BasketAggregate;

namespace CDG.BLL.Specifications.BasketSpecifications;

public sealed class BasketItemByIdSpecification : Specification<BasketItem>, ISingleResultSpecification
{
    public BasketItemByIdSpecification(int id)
    {
        Query
            .Where(b => b.Id == id);
    }
}