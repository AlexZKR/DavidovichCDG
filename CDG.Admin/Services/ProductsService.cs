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

    public async Task<T> GetBookById<T>(int id)
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            URL = SD.MainAPIBase + "/api/books?id=" + id,
            AccessToken = ""
        });
    }

    public async Task<T> GetAuthors<T>()
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            URL = SD.MainAPIBase + "/api/books/authors",
            AccessToken = ""
        });
    }

    public async Task<T> GetBooksPaged<T>(int page, int pageSize)
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            URL = SD.MainAPIBase + "/api/books?page=" + page + "&pageSize=" + pageSize,
            AccessToken = ""
        });
    }

    public async Task<T> CountBooks<T>()
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            URL = SD.MainAPIBase + "/api/books/count",
            AccessToken = ""
        });
    }


    public async Task<T> AddBook<T>(ProductDTO book)
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.POST,
            URL = SD.MainAPIBase + "/api/books/add",
            Data = book,
            AccessToken = ""
        });
    }

    public async Task<T> UpdateBook<T>(ProductDTO book)
    {
         return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.POST,
            URL = SD.MainAPIBase + "/api/books/update",
            Data = book,
            AccessToken = ""
        });
    }

    public async Task<T> DeleteBook<T>(int id)
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.DELETE,
            URL = SD.MainAPIBase + "/api/books/delete?id=" + id,
            AccessToken = ""
        });
    }

}