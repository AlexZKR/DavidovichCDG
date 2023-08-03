using Ardalis.Specification;
using CDG.BLL.Entities.Enums;
using CDG.BLL.Entities.Products;

namespace CDG.BLL.Specifications.CatalogSpecifications;

public class KeyCatalogGetNumberOfTopSoldItemsSpecification : Specification<DigitalKey>
{
    public KeyCatalogGetNumberOfTopSoldItemsSpecification(int quantity)
    {
        Query.OrderByDescending(p => p.Sold).Take(quantity);
    }
}
