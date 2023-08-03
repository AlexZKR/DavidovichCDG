#pragma warning disable CS8618 // Required by Entity Framework

using CDG.BLL.Interfaces;

namespace CDG.BLL.Entities.Order;

public class Order : BaseEntity, IAggregateRoot
{
    public bool IsInProcess { get; set; } = true;
    public int TotalItems => OrderItems.Sum(i => i.Units);
    public double TotalDiscount => OrderItems.Sum(i => i.TotalPrice - i.DiscountedPrice);
    public double TotalPrice => OrderItems.Sum(i => i.DiscountedPrice);
    public double FullPrice => OrderItems.Sum(i => i.TotalPrice);
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public OrderInfo OrderInfo { get; set; }
    public Buyer Buyer { get; set; }

    public Order(Buyer Buyer, OrderInfo OrderInfo)
    {
        this.Buyer = Buyer;
        this.OrderInfo = OrderInfo;
    }

    public Order() { }
}