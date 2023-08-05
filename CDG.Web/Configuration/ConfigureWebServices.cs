using AutoMapper;
using CDG.Web.Configuration.MapperConfig;
using CDG.Web.Interfaces;
using CDG.Web.Services;

namespace CDG.Web.Configuration;

public static class ConfigureWebServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
        //viewmodel services
        services.AddTransient<IBasketViewModelService, BasketViewModelService>();
        services.AddTransient<ICheckOutViewModelService, CheckOutViewModelService>();
        services.AddTransient<IOrderViewModelService, OrderViewModelService>();
        services.AddTransient<ICatalogViewModelService, DigitalKeyCatalogViewModelService>();
        services.AddTransient<IProductDetailsViewModelService, ProductDetailsViewModelService>();
        services.AddTransient<IRatingViewModelService, RatingViewModelService>();
        

        //automapper config
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DTOMapProfile());
            mc.AddProfile(new ViewModelMapProfile());
        });
        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);


        return services;
    }
}