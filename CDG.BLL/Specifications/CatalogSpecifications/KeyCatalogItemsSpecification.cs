using Ardalis.Specification;
using CDG.BLL.Entities.Products;

namespace CDG.BLL.Specifications.CatalogSpecifications;

public class KeyCatalogItemsSpecification : Specification<BaseProduct>
{
    public KeyCatalogItemsSpecification(params int[] ids)
    {
        Query.Where(c => ids.Contains(c.Id));
    }
}
