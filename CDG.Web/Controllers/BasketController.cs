using CDG.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CDG.Web.Extensions;
using CDG.BLL.Interfaces;
using CDG.BLL.Entities.Products;
using CDG.Web.Infrastructure;

namespace CDG.Web.Controllers;
public class BasketController : Controller
{
    private readonly IBasketViewModelService basketViewModelService;
    private readonly IDigitalKeyCatalogService DigitalKeyCatalogService;
    private readonly BLL.Interfaces.IFavouriteService<BaseProduct> favouriteService;
    private readonly IBasketService basketService;
    private readonly IBasketQueryService basketQueryService;

    public BasketController(IBasketViewModelService basketViewModelService,
    IDigitalKeyCatalogService DigitalKeyCatalogService,
    IBasketService basketService,
    IBasketQueryService basketQueryService,
    IFavouriteService<BaseProduct> favouriteService)
    {
        this.basketViewModelService = basketViewModelService;
        this.DigitalKeyCatalogService = DigitalKeyCatalogService;
        this.basketService = basketService;
        this.basketQueryService = basketQueryService;
        this.favouriteService = favouriteService;
        this.basketService = basketService;
    }
    public async Task<IActionResult> Index()
    {
        var vm = await basketViewModelService.GetOrCreateBasketForUser(ControllerBaseExtensions.GetOrSetBasketCookieAndUserName(this));
        return View(vm);
    }
    [Route("AddToCart/{id:int}")]
    public async Task<IActionResult> AddToCart(int id)
    {
        var item = await DigitalKeyCatalogService.GetDigitalKeyAsync(id);
        if (item == null)
        {
            return RedirectToPage("/Index");
        }


        var username = ControllerBaseExtensions.GetOrSetBasketCookieAndUserName(this);
        //if item was in favs, then remove it
        if(favouriteService.CheckIfFavourite(username,item) == true)
        {
            await favouriteService.RemoveFromFavourites(username,item);
        }

        var basket = await basketService.AddItemToBasket(username,
            id, item.FullPrice, item.Discount, item.Name);

        return RedirectToAction(nameof(Index));
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(int itemId, int basketId)
    {
        var username = ControllerBaseExtensions.GetOrSetBasketCookieAndUserName(this);
        basketService.RemoveItemFromBasket(username, itemId);
        var basketCount = basketQueryService.CountTotalBasketItemsAsync(username);
        if(await basketService.CheckIfEmpty(basketId) == true)
                return NoContent();
        return Ok(basketCount);
    }

    [HttpGet]
    [QueryParameterConstraint("itemId","mode")]
    public async Task<IActionResult> ChangeQuantity([FromQuery]int itemId, string mode)
    {
        string username = ControllerBaseExtensions.GetOrSetBasketCookieAndUserName(this);
        var item = await basketService.UpDownQuantity(username, itemId, mode);
        var vm = await basketViewModelService.MapBasketItem(item);
        if(await basketService.CheckIfEmpty(item.BasketId) == true)
                return NoContent();
        if(item.Quantity == 0)
        {
            return Empty;
        }
        return PartialView("_BasketCardPartial", vm);
    }

    [HttpGet]
    [Route("GetBasketInfo")]
    public async Task<IActionResult> GetBasketInfo([FromQuery]int basketId)
    {
        var basket = await basketService.GetBasketAsync(basketId);
        return Json(new
        {
            basketCount = basket.TotalItems,
            basketPrice = basket.TotalDiscountedPrice,
            basketDiscount = basket.TotalDiscountSize,
        });
    }

}