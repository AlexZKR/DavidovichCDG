using Ardalis.Specification;
using CDG.BLL.Entities.Products;
using CDG.BLL.Interfaces;

namespace CDG.BLL.Specifications.CatalogSpecifications;

public class FavouritesForUserSpecification<T> : Specification<T> where T : BaseProduct, IAggregateRoot
{
    public FavouritesForUserSpecification(string[] Ids)
    {
        Query.Where(x => Ids.Contains(x.Id.ToString()));
    }
}