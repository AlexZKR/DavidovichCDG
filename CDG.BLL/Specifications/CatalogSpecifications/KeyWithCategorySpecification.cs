using Ardalis.Specification;
using CDG.BLL.Entities.Products;

namespace CDG.BLL.Specifications.CatalogSpecifications;
public class KeyWithCategorySpecification : Specification<DigitalKey>
{
    public KeyWithCategorySpecification(int id)
    {
        Query.Where(b => b.Id == id).Include(b => b.KeyCategory);
    }
}