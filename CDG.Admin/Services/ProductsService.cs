using CDG.Admin.Interfaces;
using CDG.Admin.Models;
using CDG.Admin.Models.Product;
using Newtonsoft.Json;

namespace CDG.Admin.Services;

public class ProductService : BaseService, IProductService
{
    private readonly IHttpClientFactory clientFactory;

    public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        this.clientFactory = clientFactory;
    }

    public async Task<T> GetKeyById<T>(int id)
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            URL = SD.MainAPIBase + "/api/Keys?id=" + id,
            AccessToken = ""
        });
    }

    public async Task<T> GetCategories<T>()
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            URL = SD.MainAPIBase + "/api/Keys/categories",
            AccessToken = ""
        });
    }

    public async Task<T> GetKeysPaged<T>(int page, int pageSize)
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            URL = SD.MainAPIBase + "/api/Keys?page=" + page + "&pageSize=" + pageSize,
            AccessToken = ""
        });
    }

    public async Task<T> CountKeys<T>()
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            URL = SD.MainAPIBase + "/api/Keys/count",
            AccessToken = ""
        });
    }


    public async Task<T> AddKey<T>(ProductDTO Key)
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.POST,
            URL = SD.MainAPIBase + "/api/Keys/add",
            Data = Key,
            AccessToken = ""
        });
    }

    public async Task<T> UpdateKey<T>(ProductDTO Key)
    {
         return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.POST,
            URL = SD.MainAPIBase + "/api/Keys/update",
            Data = Key,
            AccessToken = ""
        });
    }

    public async Task<T> DeleteKey<T>(int id)
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.DELETE,
            URL = SD.MainAPIBase + "/api/Keys/delete?id=" + id,
            AccessToken = ""
        });
    }

}