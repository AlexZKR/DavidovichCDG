using Ardalis.Specification;
using CDG.BLL.Entities.Products;
using CDG.BLL.Interfaces;

namespace CDG.BLL.Specifications.CatalogSpecifications;

public class GetRatingByUsernameAndProdIdSpecification  : Specification<ProductRating>
{
    public GetRatingByUsernameAndProdIdSpecification (string username, int productId)
    {
        Query.Where(x =>
        (x.Username == username) && (x.ProductId == productId));
    }
}
