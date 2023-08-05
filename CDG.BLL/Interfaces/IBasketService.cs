using Ardalis.Result;
using CDG.BLL.Entities.BasketAggregate;
using CDG.BLL.Entities.Products;

namespace CDG.BLL.Interfaces;

public interface IBasketService
{
    Task TransferBasketAsync(string anonymousId, string userName);
    Task<Basket> AddItemToBasket(string username, int catalogItemId, double price, double discount, string productName, int quantity = 1);
    void RemoveItemFromBasket(string username, int id);
    Task<Result<Basket>> SetQuantities(int basketId, Dictionary<string, int> quantities);
    Task<BasketItem> UpDownQuantity(string username, int itemId, string mode);
    Task DeleteBasketAsync(int basketId);
    Task DeleteBasketAsync(string buyerId);
    Task<Basket> GetBasketAsync(int basketId);
    Task<Basket> GetBasketAsync(string username);
    Task<List<BaseProduct>> GetBasketItemsAsync(string username);
    Task<List<BaseProduct>> GetBasketItemsAsync(int basketId);
    Task<BasketItem> GetBasketItemAsync(int id);
    Task<bool> CheckIfEmpty(int basketId);
}
