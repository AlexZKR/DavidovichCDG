using System.Reflection;
using CDG.BLL.Entities.BasketAggregate;
using CDG.BLL.Entities.Order;
using CDG.BLL.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CDG.DAL.Data;

#pragma warning disable CS8618 // Disabling null value warnings
public class AppDbContext : DbContext
{
    //basket tables
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }

    //order tables
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    //product tables
    public DbSet<BaseProduct> BaseProducts { get; set; }
    public DbSet<UserFavourites> Favourites { get; set; }
    public DbSet<ProductRating> Ratings { get; set; }
    public DbSet<DigitalKey> DigitalKeys { get; set; }
    public DbSet<KeyCategory> KeyCategorys { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);
        //order value objects configuration
        builder.Entity<Order>().OwnsOne(o => o.OrderInfo);
        builder.Entity<Order>().OwnsOne(o => o.Buyer);

        //builder.Entity<Order>().HasMany(o => o.OrderItems);


    }
}
