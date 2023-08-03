using Ardalis.Specification;
using CDG.BLL.Entities.Products;
using CDG.BLL.Interfaces;

namespace CDG.BLL.Specifications.CatalogSpecifications;

public class GetRatingsByProdIdSpecification : Specification<ProductRating>
{

    public GetRatingsByProdIdSpecification(int productId)
    {
        Query.Where(x => x.ProductId == productId);
    }
}