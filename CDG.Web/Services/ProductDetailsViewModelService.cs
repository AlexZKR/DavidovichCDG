using CDG.BLL.Entities.Enums;
using CDG.BLL.Entities.Products;
using CDG.BLL.Interfaces;
using CDG.BLL.Specifications.CatalogSpecifications;
using CDG.Web.Infrastructure;
using CDG.Web.Models.Catalog;

namespace CDG.Web.Services;

public class ProductDetailsViewModelService : IProductDetailsViewModelService
{
    private readonly ILogger<DigitalKeyCatalogViewModelService> logger;
    private readonly IRepository<DigitalKey> DigitalKeyRepository;
    private readonly IImageService uriComposer;
    private readonly IFavouriteService<DigitalKey> favouriteService;

    public ProductDetailsViewModelService(ILoggerFactory loggerFactory,
    IRepository<DigitalKey> DigitalKeyRepository,
    IImageService uriComposer,
    IFavouriteService<DigitalKey> favouriteService)
    {
        logger = loggerFactory.CreateLogger<DigitalKeyCatalogViewModelService>();
        this.DigitalKeyRepository = DigitalKeyRepository;
        this.uriComposer = uriComposer;
        this.favouriteService = favouriteService;
    }

    public async Task<ProductDetailsViewModel> CreateViewModel(int id, string username)
    {
        var spec = new KeyWithCategorySpecification(id);
        var item = await DigitalKeyRepository.FirstOrDefaultAsync(spec);
        var vm = new ProductDetailsViewModel()
        {
            Id = id,
            Name = item!.Name,
            Description = item.Description,
            KeyCategoryName = item.KeyCategory.Name,
            PictureUrl = item.PictureUri,
            Price = item.FullPrice,
            DiscountedPrice = item.DiscountedPrice,
            Discount = item.Discount,
            IsAvailable = false,
            IsOnDiscount = false,
            IsFavourite = favouriteService.CheckIfFavourite(username, item),

            //nav data
            KeyCategoryId = item.KeyCategory.Id,
            Username = username
        };
        if (item.Quantity > 0) vm.IsAvailable = true;
        if (item.Discount > 0) vm.IsOnDiscount = true;
        return vm;
    }


}
