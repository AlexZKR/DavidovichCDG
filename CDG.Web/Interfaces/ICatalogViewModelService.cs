using CDG.BLL;
using CDG.Web.Models.Catalog;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace CDG.Web.Interfaces;

public interface ICatalogViewModelService
{
    Task<CatalogViewModel> GetCatalogViewModel(string username,string? searchQuery, int pageIndex = 0, int itemsPage = SD.ITEMS_PER_PAGE,  int KeyCategoryId = 0, int? cover = null, int? genre = null, int? lang = null);
    Task<IEnumerable<SelectListItem>> GetKeyCategorysSelectList();
    Task<CatalogViewModel> GetTopSoldItemsViewModel(int quantity, string username);
    Task<CatalogItemViewModel> GetCatalogItemViewModelAsync(int prodId);

}
