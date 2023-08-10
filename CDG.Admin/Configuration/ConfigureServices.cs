using AutoMapper;
using CDG.Admin.Configuration.MapperConfig;
using CDG.Admin.Interfaces;
using CDG.Admin.Services;

namespace CDG.Admin.Configuration;

public static class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddHttpClient<IOrderService, OrderService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddHttpClient<IProductService, ProductService>();
        services.AddScoped<IProductService, ProductService>();

        //automapper config
        var mapperConfig = new MapperConfiguration(mc => {
            mc.AddProfile(new DTOMapProfile());
        });
        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}