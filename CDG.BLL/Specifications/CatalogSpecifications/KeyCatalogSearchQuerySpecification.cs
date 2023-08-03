using Ardalis.Specification;
using CDG.BLL.Entities.Products;

namespace CDG.BLL.Specifications.CatalogSpecifications;
public class KeyCatalogSearchQuerySpecification : Specification<DigitalKey>
{
    public KeyCatalogSearchQuerySpecification(string searchQuery)
    {
        Query.Include(b => b.KeyCategory).Where(b =>
            (b.Name.ToLower().Contains(searchQuery.ToLower())) ||
            (b.Name.ToLower() == searchQuery.ToLower()) ||
            (b.KeyCategory.Name!.ToLower()!.Contains(searchQuery.ToLower())))
        .OrderBy(n => n.Name);
    }
}
