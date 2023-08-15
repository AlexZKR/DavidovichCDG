using AutoMapper;
using CDG.BLL;
using CDG.BLL.Entities.Enums;
using CDG.BLL.Entities.Products;
using CDG.BLL.Interfaces;
using CDG.Web.Infrastructure;
using CDG.Web.Interfaces;
using CDG.Web.Models.Catalog;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CDG.Web.Services;

public class DigitalKeyCatalogViewModelService : ICatalogViewModelService
{
    private readonly ILogger<DigitalKeyCatalogViewModelService> logger;
    private readonly IDigitalKeyCatalogService DigitalKeyCatalogService;
    private readonly IFavouriteService<DigitalKey> favouriteService;
    private readonly IRatingService ratingService;
    private readonly IMapper mapper;

    public DigitalKeyCatalogViewModelService(ILoggerFactory loggerFactory,
    IDigitalKeyCatalogService DigitalKeyCatalogService,
    IFavouriteService<DigitalKey> favouriteService,
    IRatingService ratingService,
    IMapper mapper)
    {
        logger = loggerFactory.CreateLogger<DigitalKeyCatalogViewModelService>();
        this.DigitalKeyCatalogService = DigitalKeyCatalogService;
        this.favouriteService = favouriteService;
        this.ratingService = ratingService;
        this.mapper = mapper;
    }

    public async Task<CatalogViewModel> GetCatalogViewModel(string username, string? searchQuery, int pageIndex = 0, int itemsPage = SD.ITEMS_PER_PAGE, int category = 0)
    {
        logger.LogInformation("GetCatalogItemsViewModels called");

        List<DigitalKey> itemsOnPage = await DigitalKeyCatalogService.GetCatalogItems(username, searchQuery, pageIndex, itemsPage, category);

        var vm = new CatalogViewModel()
        {
            CatalogItems = itemsOnPage.Select(b => new CatalogItemViewModel()
            {
                Id = b.Id,
                Name = b.Name,
                KeyCategoryName = b.KeyCategory.Name,
                PictureUri = b.PictureUri,
                Price = b.FullPrice,
                DiscountedPrice = b.DiscountedPrice,
                IsOnDiscount = b.Discount != 0,
                IsAvailable = b.Quantity != 0,
                IsFavourite = favouriteService.CheckIfFavourite(username, b),
                Rating = new RatingViewModel { Rating = ratingService.GetRating(username, b.Id).Result },
            }).ToList(),
            FilterInfo = new CatalogFilterViewModel()
            {
                SearchQuery = searchQuery,
                KeyCategorys = (await GetKeyCategorysSelectList()).ToList(),
            },
            PaginationInfo = new PaginationViewModel()
            {
                ActualPage = pageIndex,
                ItemsOnPage = itemsOnPage.Count,
                TotalItems = await DigitalKeyCatalogService.TotalItemsCountAsync(searchQuery, category, pageIndex, itemsPage),
            }
        };

        vm.PaginationInfo.IsNextPageHasItems = (vm.PaginationInfo.TotalItems - SD.ITEMS_PER_PAGE * vm.PaginationInfo.ActualPage) > 0 ? true : false;
        vm.PaginationInfo.TotalPages = CalculateTotalPages(vm.PaginationInfo.TotalItems, SD.ITEMS_PER_PAGE);
        vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
        vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

        vm.PaginationInfo.IsNextPageHasItems = vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1 ? false : true;

        return vm;
    }

    public async Task<CatalogViewModel> GetTopSoldItemsViewModel(int quantity, string username)
    {
        var products = await DigitalKeyCatalogService.GetTopSoldItems(quantity, username);

        var vm = new CatalogViewModel()
        {
            CatalogItems = products.Select(p => new CatalogItemViewModel
            {
                Id = p.Id,
                Name = p.Name,
                PictureUri = p.PictureUri,
                Price = p.FullPrice,
                DiscountedPrice = p.DiscountedPrice,
                IsFavourite = favouriteService.CheckIfFavourite(username, p),
                IsOnDiscount = p.Discount > 0 ? true : false,

            }).ToList(),
        };
        return vm;
    }
    public async Task<CatalogItemViewModel> GetCatalogItemViewModelAsync(int prodId)
    {
        var DigitalKey = await DigitalKeyCatalogService.GetDigitalKeyAsync(prodId);
        var vm = mapper.Map<CatalogItemViewModel>(DigitalKey);
        return vm;
    }


    public async Task<IEnumerable<SelectListItem>> GetKeyCategorysSelectList()
    {
        logger.LogInformation("GetKeyCategorys called");
        var KeyCategoryNames = await DigitalKeyCatalogService.GetKeyCategorys();

        var items = KeyCategoryNames.Select(n => new SelectListItem() { Text = n.Name, Value = n.Id.ToString() })
            .OrderBy(n => n.Text)
            .ToList();

        var allItem = new SelectListItem() { Value = "0", Text = "Все", Selected = true };
        items.Insert(0, allItem);
        return items;
    }

    private int CalculateTotalPages(int totalItems, int ItemsPerPage)
    {
        int res = 0;
        res = (int)Math.Ceiling((double)totalItems / (double)ItemsPerPage);
        logger.LogInformation($"Total pages: {res}");
        return res;
    }
}