
using CDG.BLL.Entities.Products;
using CDG.BLL.Exceptions;
using CDG.BLL.Interfaces;
using CDG.BLL.Specifications.CatalogSpecifications;

namespace CDG.BLL.Services;

public class ImageService : IImageService
{
    private readonly IRepository<BaseProduct> productRepository;

    public ImageService(IRepository<BaseProduct> productRepository)
    {
        this.productRepository = productRepository;
    }

        public async Task<string> ComposePicUriById(int id)
    {
        var product = await productRepository.FirstOrDefaultAsync(new BaseProductSpecification(id));
        if(product == null) throw new NotFoundException($"Product with id {id} was not found");
        return product.PictureUri;
    }
}
