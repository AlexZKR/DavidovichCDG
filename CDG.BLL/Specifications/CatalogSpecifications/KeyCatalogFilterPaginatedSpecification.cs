using Ardalis.Specification;
using CDG.BLL.Entities.Products;

namespace CDG.BLL.Specifications.CatalogSpecifications;

public class KeyCatalogFilterPaginatedSpecification : Specification<DigitalKey>
{
    public KeyCatalogFilterPaginatedSpecification(int skip, int take, int? CategoryId) : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query.Where(i =>
            !(CategoryId.HasValue && CategoryId != 0) || (int)i.CategoryId == CategoryId)
            .Include(b => b.KeyCategory)
            .Skip(skip).Take(take);
    }
}
