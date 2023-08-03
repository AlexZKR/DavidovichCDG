using CDG.BLL.Entities.Products;
using CDG.BLL.Exceptions;
using CDG.BLL.Interfaces;
using CDG.BLL.Specifications.CatalogSpecifications;

namespace CDG.BLL.Services;

public class DigitalKeyCatalogService : IDigitalKeyCatalogService
{
    private readonly IAppLogger<DigitalKeyCatalogService> logger;
    private readonly IRepository<DigitalKey> DigitalKeyRepository;
    private readonly IRepository<KeyCategory> KeyCategoryRepository;

    public DigitalKeyCatalogService(IAppLogger<DigitalKeyCatalogService> logger,
                              IRepository<DigitalKey> DigitalKeyRepository,
                              IRepository<KeyCategory> KeyCategoryRepository)
    {
        this.logger = logger;
        this.DigitalKeyRepository = DigitalKeyRepository;
        this.KeyCategoryRepository = KeyCategoryRepository;
    }
    public async Task<List<DigitalKey>> GetCatalogItems(string username,
                                                  string? searchQuery,
                                                  int pageIndex = 0,
                                                  int itemsPage = SD.ITEMS_PER_PAGE,
                                                  int? KeyCategoryId = 0,
                                                  int? cover = null,
                                                  int? genre = null,
                                                  int? lang = null)
    {
        logger.LogInformation("GetCatalogItems called");

        List<DigitalKey> itemsOnPage;
        if (searchQuery == null)
        {
            var paginatedFilterSpec =
             new KeyCatalogFilterPaginatedSpecification(skip: itemsPage * pageIndex, take: itemsPage, KeyCategoryId, cover, genre, lang);

            itemsOnPage = await DigitalKeyRepository.ListAsync(paginatedFilterSpec);
        }
        else
        {
            var filterSearchQuerySpec =
            new KeyCatalogSearchQuerySpecification(searchQuery);
            itemsOnPage = await DigitalKeyRepository.ListAsync(filterSearchQuerySpec);
        }
        return itemsOnPage;
    }

    public async Task<List<DigitalKey>> GetTopSoldItems(int quantity, string username)
    {
        var spec = new KeyCatalogGetNumberOfTopSoldItemsSpecification(quantity);
        var products = await DigitalKeyRepository.ListAsync(spec);
        return products!;
    }

    public async Task<IEnumerable<KeyCategory>> GetKeyCategorys()
    {
        logger.LogInformation("GetKeyCategorys called");
        var KeyCategorys = await KeyCategoryRepository.ListAsync();
        return KeyCategorys;
    }



    public async Task<int> TotalItemsCountAsync(string? searchQuery,
                                                int? KeyCategoryId,
                                                int? cover,
                                                int? genre,
                                                int? lang,
                                                int pageIndex = 0,
                                                int itemsPage = SD.ITEMS_PER_PAGE)
    {

        if (searchQuery == null)
        {
            var paginatedFilterSpec = new KeyCatalogFilterPaginatedSpecification(skip: itemsPage
                * pageIndex, take: itemsPage, KeyCategoryId, cover, genre, lang);
            int q = await DigitalKeyRepository.CountAsync(paginatedFilterSpec);
            logger.LogInformation($"CountAsync: {q} DigitalKeys in DB");
            return q;
        }
        else
        {
            var filterSearchQuerySpec = new KeyCatalogSearchQuerySpecification(searchQuery);
            return await DigitalKeyRepository.CountAsync(filterSearchQuerySpec);
        }
        throw new NotFoundException("Error when counting total items");
    }


    public async Task<DigitalKey> GetDigitalKeyAsync(int id)
    {
        var spec = new KeyWithCategorySpecification(id);
        var DigitalKey = await DigitalKeyRepository.FirstOrDefaultAsync(spec);
        if(DigitalKey == null) throw new NotFoundException($"DigitalKey with id {id} was not found");
        return DigitalKey;
    }

    public async Task<List<DigitalKey>> GetDigitalKeysPaged(int pageNo, int pageSize)
    {
        var spec = new KeyPaginatedSpecification(pageNo * pageSize, pageSize);
        var DigitalKeys = await DigitalKeyRepository.ListAsync(spec);

        return DigitalKeys;
    }

    public async Task<int> CountDigitalKeysAsync() => await DigitalKeyRepository.CountAsync();

    public async Task<DigitalKey> AddDigitalKeyAsync(DigitalKey DigitalKey)
    {
        return DigitalKey = await DigitalKeyRepository.AddAsync(DigitalKey);
    }
    public async Task<DigitalKey> UpdateDigitalKeyAsync(DigitalKey DigitalKey)
    {
        await DigitalKeyRepository.UpdateAsync(DigitalKey);
        return DigitalKey;
    }
    public async void DeleteDigitalKeyAsync(int id)
    {
        var DigitalKey = await GetDigitalKeyAsync(id);
        await DigitalKeyRepository.DeleteAsync(DigitalKey);
    }

}