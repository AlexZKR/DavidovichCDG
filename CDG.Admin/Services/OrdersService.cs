using CDG.Admin.Interfaces;
using CDG.Admin.Models;

namespace CDG.Admin.Services;

public class OrderService : BaseService, IOrderService
{
    private readonly IHttpClientFactory clientFactory;

    public OrderService(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        this.clientFactory = clientFactory;
    }
    public async Task<T> GetAllBuyersAsync<T>()
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            URL = SD.MainAPIBase + "/api/orders/buyers",
            AccessToken = ""
        });
    }

    public async Task<T> GetOrderDetails<T>(int id)
    {
        return await this.SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            URL = SD.MainAPIBase + "/api/orders?orderId=" + id,
            AccessToken = ""
        });
    }

    public async Task<T> GetUserWithOrdersByIdAsync<T>(string id)
    {
       return await this.SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,
                URL = SD.MainAPIBase + "/api/orders?buyer=" + id,
                AccessToken = ""
            });
    }

    public async Task<T> ApproveOrder<T>(int id)
    {
        return await this.SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.POST,
            URL = SD.MainAPIBase + "/api/orders/approve?orderId=" + id,
            AccessToken = ""
        });
    }

}