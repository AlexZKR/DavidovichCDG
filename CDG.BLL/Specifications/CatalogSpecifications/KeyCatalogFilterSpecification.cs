using Ardalis.Specification;
using CDG.BLL.Entities.Enums;
using CDG.BLL.Entities.Products;

namespace CDG.BLL.Specifications.CatalogSpecifications;

public class KeyCatalogFilterSpecification : Specification<DigitalKey>
{
    public KeyCatalogFilterSpecification(int? CategoryId, int? cover, int? genre, int? lang)
    {
        Query.Where(i =>
            !(CategoryId.HasValue || i.CategoryId == CategoryId))
            .Include(b => b.KeyCategory);
    }
}
