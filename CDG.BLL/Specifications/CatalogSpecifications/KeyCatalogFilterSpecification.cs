using Ardalis.Specification;
using CDG.BLL.Entities.Enums;
using CDG.BLL.Entities.Products;

namespace CDG.BLL.Specifications.CatalogSpecifications;

public class KeyCatalogFilterSpecification : Specification<DigitalKey>
{
    public KeyCatalogFilterSpecification(int? CategoryId, int? cover, int? genre, int? lang)
    {
        Query.Where(i =>
            (!CategoryId.HasValue || i.CategoryId == CategoryId) &&
            // (!cover.HasValue || (int)i.Cover == cover) &&
            // (!genre.HasValue || (int)i.Genre == genre) &&
            (!lang.HasValue || (int)i.Language == lang))
            .Include(b => b.KeyCategory);
    }
}
