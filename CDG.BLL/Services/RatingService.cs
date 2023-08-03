using CDG.BLL.Entities.Products;
using CDG.BLL.Exceptions;
using CDG.BLL.Interfaces;
using CDG.BLL.Specifications.CatalogSpecifications;

namespace CDG.BLL.Services;

public class RatingService : IRatingService
{
    private readonly IRepository<BaseProduct> productRepository;
    private readonly IRepository<ProductRating> ratingRepository;
    private readonly IAppLogger<RatingService> logger;

    public RatingService(IRepository<BaseProduct> productRepository,
    IRepository<ProductRating> ratingRepository,
    IAppLogger<RatingService> logger)
    {
        this.productRepository = productRepository;
        this.ratingRepository = ratingRepository;
        this.logger = logger;
    }

    public async Task<double> UpdateProductAverageRating(int productId)
    {
        var product = await GetProduct(productId);

        var spec = new GetRatingsByProdIdSpecification(productId);
        var ratings = await ratingRepository.ListAsync(spec);

        product.Rating = (ratings.Sum(x => x.Rating)) / ratings.Count;

        await productRepository.UpdateAsync(product);

        return product.Rating;
    }

    public async Task<int> GetRating(string username, int productId)
    {
        try
        {
            var spec = new GetRatingByUsernameAndProdIdSpecification(username, productId);
        var rating = await ratingRepository.FirstOrDefaultAsync(spec);
        if(rating == null)
            throw new NotFoundException($"Rating record for product with id {productId} and user {username} was not found");
        return rating.Rating;
        }
        catch (Exception ex)
        {
            if(ex is NotFoundException)
            {
                logger.LogWarning(ex.Message);
                return 5;
            }
            throw;
        }
    }

    public async Task<int> SetRating(string username, int productId, int rating)
    {
        var productRating = new ProductRating
        {
            Username = username,
            ProductId = productId,
            Rating = rating,
        };
        await ratingRepository.AddAsync(productRating);
        return rating;
    }

    //private helper
    private async Task<BaseProduct> GetProduct(int productId)
    {
        var spec = new BaseProductSpecification(productId);
        var item = await productRepository.FirstOrDefaultAsync(spec);
        if(item == null) throw new NotFoundException($"Product with id {productId} was not found");
        return item;
    }
}