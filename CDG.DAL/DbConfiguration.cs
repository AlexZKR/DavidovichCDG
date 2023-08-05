using CDG.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CDG.DAL;

public static class DbConfiguration
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        bool UseSqlite = false;
        if (configuration["DbConfig:UseSqlite"] != null)
        {
            UseSqlite = bool.Parse(configuration["DbConfig:UseSqlite"]!);
        }
        if (UseSqlite)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("sqliteAppDbContext")));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("sqliteIdentityDbContext")));
        }
        else
        {
           services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("sqlAppDbContext")));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("sqlIdentityDbContext")));
        }
    }
}