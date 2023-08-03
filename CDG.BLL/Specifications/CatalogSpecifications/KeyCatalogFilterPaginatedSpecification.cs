using Ardalis.Specification;
using CDG.BLL.Entities.Products;

namespace CDG.BLL.Specifications.CatalogSpecifications;

public class KeyCatalogFilterPaginatedSpecification : Specification<DigitalKey>
{
    public KeyCatalogFilterPaginatedSpecification(int skip, int take, int? CategoryId, int? cover, int? genre, int? lang) : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query.Where(i =>
            // (!(cover.HasValue && cover != 0) || (int)i.Cover == cover) &&
            // (!(genre.HasValue && genre != 0) || (int)i.Genre == genre) &&
            (!(CategoryId.HasValue && CategoryId != 0) || (int)i.CategoryId == CategoryId) &&
            (!(lang.HasValue && lang != 0) || (int)i.Language == lang))
            .Include(b => b.KeyCategory)
            .Skip(skip).Take(take);
    }
}
