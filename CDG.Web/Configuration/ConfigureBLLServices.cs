using CDG.BLL;
using CDG.BLL.Entities.Products;
using CDG.BLL.Interfaces;
using CDG.BLL.Services;
using CDG.DAL.Data;
using CDG.DAL.Data.Queries;
using CDG.DAL.Logging;
using CDG.DAL.Services;

namespace CDG.Web.Configuration;

public static class ConfigureBLLServices
{
    public static IServiceCollection AddBLLServices(this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddScoped(typeof(IReadRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IDigitalKeyCatalogService, DigitalKeyCatalogService>();

        services.AddTransient(typeof(IFavouriteService<>), typeof(FavouriteService<>));
        services.AddTransient<IRatingService, RatingService>();

        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IBasketQueryService, BasketQueryService>();
        services.AddScoped<IOrderService, OrderService>();

        services.AddScoped<IImageService, ImageService>();
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));


        return services;
    }
}