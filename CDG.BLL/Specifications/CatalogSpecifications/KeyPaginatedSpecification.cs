using Ardalis.Specification;
using CDG.BLL.Entities.Products;

namespace CDG.BLL.Specifications.CatalogSpecifications;

public class KeyPaginatedSpecification : Specification<DigitalKey>
{
    public KeyPaginatedSpecification(int skip, int take) : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query.Include(b => b.KeyCategory).Skip(skip).Take(take);
    }
}
