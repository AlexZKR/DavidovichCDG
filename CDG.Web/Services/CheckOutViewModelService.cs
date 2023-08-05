using Ardalis.GuardClauses;
using CDG.BLL.Entities.BasketAggregate;
using CDG.BLL.Entities.Enums;
using CDG.BLL.Entities.Order;
using CDG.BLL.Entities.Products;
using CDG.BLL.Extensions;
using CDG.BLL.Interfaces;
using CDG.BLL.Specifications;
using CDG.BLL.Specifications.CatalogSpecifications;
using CDG.Web.Infrastructure;
using CDG.Web.Interfaces;
using CDG.Web.Models.Order;

namespace CDG.Web.Services;

public class CheckOutViewModelService : ICheckOutViewModelService
{
    private readonly IImageService uriComposer;
    private readonly IRepository<Basket> basketRepository;
    private readonly IRepository<BaseProduct> itemRepository;

    public CheckOutViewModelService(IRepository<Basket> basketRepository,
        IRepository<BaseProduct> itemRepository,
        IRepository<Order> orderRepository,
        IImageService uriComposer)
    {
        this.basketRepository = basketRepository;
        this.itemRepository = itemRepository;
        this.uriComposer = uriComposer;
    }

    public async Task<CheckOutViewModel> CreateCheckOutVMAsync(string username)
    {
        var basketSpec = new BasketWithItemsSpecification(username);
        var basket = await basketRepository.FirstOrDefaultAsync(basketSpec);

        Guard.Against.Null(basket, nameof(basket));
        Guard.Against.EmptyBasketOnCheckout(basket.Items);

        var catalogItemsSpecification = new KeyCatalogItemsSpecification(basket.Items.Select(item => item.ProductId).ToArray());
        var catalogItems = await itemRepository.ListAsync(catalogItemsSpecification);


        var vm = new CheckOutViewModel()
        {
            BuyerId = username,
            TotalPrice = basket.Items.Sum(i => i.FullPrice),
            TotalItems = basket.TotalItems,

            PaymentTypes = EnumHelper<PaymentType>.GetStaticDataFromEnum(PaymentType.Cash).ToList(),

            OrderItems = catalogItems.Select(i => new OrderItemViewModel()
            {
                ProductId = i.Id,
                ProductName = i.Name,
                FullPrice = i.FullPrice,
                DiscountedPrice = i.DiscountedPrice,
                Discount = i.Discount,
                Units = basket.Items.FirstOrDefault(bi => bi.ProductId == i.Id)!.Quantity,
                PictureUrl = i.PictureUri,

            }).ToList(),
        };

        return vm;

    }



}
