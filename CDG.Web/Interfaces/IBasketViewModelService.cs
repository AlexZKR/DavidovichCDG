using CDG.BLL.Entities.BasketAggregate;
using CDG.Web.Models.Basket;

namespace CDG.Web.Interfaces;

public interface IBasketViewModelService
{
    Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
    Task<int> CountTotalBasketItems(string username);
    Task<BasketViewModel> Map(Basket basket);
    Task<BasketItemViewModel> MapBasketItem(BasketItem item);

}
