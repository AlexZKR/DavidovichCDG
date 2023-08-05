using Ardalis.GuardClauses;
using Ardalis.Result;
using CDG.BLL.Entities.BasketAggregate;
using CDG.BLL.Entities.Products;
using CDG.BLL.Exceptions;
using CDG.BLL.Interfaces;
using CDG.BLL.Specifications;
using CDG.BLL.Specifications.BasketSpecifications;

namespace CDG.BLL.Services;

public class BasketService : IBasketService
{
    private readonly IRepository<Basket> basketRepository;
    private readonly IAppLogger<BasketService> logger;
    private readonly IRepository<BaseProduct> productRepository;
    private readonly IRepository<BasketItem> basketItemRepository;

    public BasketService(IRepository<Basket> basketRepository,
        IAppLogger<BasketService> logger,
        IRepository<BaseProduct> productRepository,
        IRepository<BasketItem> basketItemRepository)
    {
        this.basketRepository = basketRepository;
        this.logger = logger;
        this.productRepository = productRepository;
        this.basketItemRepository = basketItemRepository;
    }

    public async Task<Basket> AddItemToBasket(string username, int catalogItemId, double price, double discount, string productName, int quantity = 1)
    {
        var basketSpec = new BasketWithItemsSpecification(username);
        var basket = await basketRepository.FirstOrDefaultAsync(basketSpec);

        if (basket == null)
        {
            basket = new Basket(username);
            await basketRepository.AddAsync(basket);
        }

        basket.AddItem(catalogItemId, price, discount, productName, quantity);

        await basketRepository.UpdateAsync(basket);
        return basket;
    }

    public async Task<BasketItem> UpDownQuantity(string username, int itemId, string mode)
    {
        var basket = await GetBasketAsync(username);
        var item = basket.Items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            switch (mode)
            {
                case "add":
                    item.AddQuantity(1);
                    break;
                case "sub":
                    item.DecreaseQuantity(1);
                    if (item.Quantity == 0)
                        basket.RemoveEmptyItems();
                    break;
            }
            await basketRepository.UpdateAsync(basket);
            return item;
        }
        throw new Exceptions.NotFoundException($"Basket item with id {itemId} was not found");
    }

    public async void RemoveItemFromBasket(string username, int id)
    {
        var basket = await GetBasketAsync(username);

        Guard.Against.Null(basket, nameof(basket));
        var item = basket.Items.Where(i => i.Id == id).FirstOrDefault();

        if (item != null)
        {
            item.SetQuantity(0);
            basket.RemoveEmptyItems();
            await basketRepository.UpdateAsync(basket);
        }
    }
    public async Task DeleteBasketAsync(int basketId)
    {
        var basket = await GetBasketAsync(basketId);
        Guard.Against.Null(basket, nameof(basket));
        await basketRepository.DeleteAsync(basket);
    }
    public async Task DeleteBasketAsync(string buyerId)
    {
        var basket = await GetBasketAsync(buyerId);
        Guard.Against.Null(basket, nameof(basket));
        await basketRepository.DeleteAsync(basket);
    }

    public async Task<Result<Basket>> SetQuantities(int basketId, Dictionary<string, int> quantities)
    {
        var basket = await GetBasketAsync(basketId);


        if (basket == null) return Result<Basket>.NotFound();

        foreach (var item in basket.Items)
        {
            if (quantities.TryGetValue(item.Id.ToString(), out var quantity))
            {
                if (logger != null) logger.LogInformation($"Updating quantity of item ID:{item.Id} to {quantity}.");
                item.SetQuantity(quantity);
            }
        }
        basket.RemoveEmptyItems();
        await basketRepository.UpdateAsync(basket);
        return basket;
    }

    public async Task TransferBasketAsync(string anonymousId, string userName)
    {
        //separate logic for querying anonym basket
        var basketSpec = new BasketWithItemsSpecification(anonymousId);
        var anonymousBasket = await basketRepository.FirstOrDefaultAsync(basketSpec);
        if (anonymousBasket == null) return;


        var userBasketSpec = new BasketWithItemsSpecification(userName);
        var userBasket = await basketRepository.FirstOrDefaultAsync(userBasketSpec);

        if (userBasket == null)
        {
            userBasket = new Basket(userName);
            await basketRepository.AddAsync(userBasket);
        }

        foreach (var item in anonymousBasket.Items)
        {
            userBasket.AddItem(item.ProductId, item.FullPrice, item.Discount, item.ProductName!, item.Quantity);
        }

        await basketRepository.UpdateAsync(userBasket);
        await basketRepository.DeleteAsync(anonymousBasket);
    }

    public async Task<Basket> GetBasketAsync(string username)
    {
        var basketSpec = new BasketWithItemsSpecification(username);
        var basket = await basketRepository.FirstOrDefaultAsync(basketSpec);
        if (basket != null)
            return basket;
        throw new BasketNotFoundException(username);
    }
    public async Task<Basket> GetBasketAsync(int busketId)
    {
        var basketSpec = new BasketWithItemsSpecification(busketId);
        var basket = await basketRepository.FirstOrDefaultAsync(basketSpec);
        if (basket != null)
            return basket;
        throw new BasketNotFoundException(busketId);
    }

    public async Task<BasketItem> GetBasketItemAsync(int id)
    {
        var itemSpec = new BasketItemByIdSpecification(id);
        var item = await basketItemRepository.FirstOrDefaultAsync(itemSpec);
        if (item != null)
            return item;
        throw new Exceptions.NotFoundException($"Basket item with id {id} was not found");
    }

    public async Task<List<BaseProduct>> GetBasketItemsAsync(string username)
    {
        var basket = await GetBasketAsync(username);
        var list = new List<BaseProduct>();
        foreach (var item in basket.Items)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId);
            if (product != null)
                list.Add(product);
            else
                throw new Exceptions.NotFoundException($"Item with id {item.ProductId} not found in db");
        }
        return list;
    }
    public async Task<List<BaseProduct>> GetBasketItemsAsync(int busketId)
    {
        var basket = await GetBasketAsync(busketId);
        var list = new List<BaseProduct>();
        foreach (var item in basket.Items)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId);
            if (product != null)
                list.Add(product);
            else
                throw new Exceptions.NotFoundException($"Item with id {item.ProductId} not found in db");
        }
        return list;
    }

    public async Task<bool> CheckIfEmpty(int basketId)
    {
        var basket = await GetBasketAsync(basketId);
        if(basket.Items.Count == 0)
            return true;
        if(basket.Items.All(i => i.Quantity > 0) == false)
            return true;
        return false;
    }

}