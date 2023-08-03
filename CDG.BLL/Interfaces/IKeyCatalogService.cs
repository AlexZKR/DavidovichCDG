using CDG.BLL.Entities.Products;

namespace CDG.BLL.Interfaces;

public interface IDigitalKeyCatalogService
{
    Task<List<DigitalKey>> GetCatalogItems(string username, string? searchQuery, int pageIndex = 0, int itemsPage = SD.ITEMS_PER_PAGE, int? KeyCategoryId = 0, int? cover = null, int? genre = null, int? lang = null);
    Task<int> TotalItemsCountAsync(string? searchQuery, int? KeyCategoryId, int? cover, int? genre, int? lang, int pageIndex = 0, int itemsPage = SD.ITEMS_PER_PAGE);
    Task<List<DigitalKey>> GetTopSoldItems(int quantity, string username);
    Task<IEnumerable<KeyCategory>> GetKeyCategorys();

    Task<int> CountDigitalKeysAsync();
    Task<DigitalKey> GetDigitalKeyAsync(int id);
    Task<List<DigitalKey>> GetDigitalKeysPaged(int pageNo, int pageSize);
    Task<DigitalKey> AddDigitalKeyAsync(DigitalKey DigitalKey);
    Task<DigitalKey> UpdateDigitalKeyAsync(DigitalKey DigitalKey);
    void DeleteDigitalKeyAsync(int id);

}
