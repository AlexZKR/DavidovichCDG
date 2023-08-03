using Ardalis.Specification;
using CDG.BLL.Entities.Products;

namespace CDG.BLL.Specifications.CatalogSpecifications;

public class GetFavouritesSpecification : Specification<UserFavourites>
{
    public GetFavouritesSpecification(string username)
    {
        Query.Where(f => f.Username == username);
    }
}
