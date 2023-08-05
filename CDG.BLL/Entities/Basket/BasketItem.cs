using CDG.BLL.Interfaces;

namespace CDG.BLL.Entities.BasketAggregate;

public class BasketItem : BaseEntity, IAggregateRoot
{
    public double FullPrice {get; private set;}
    public double TotalPrice => FullPrice * Quantity;
    public double Discount { get; private set; }
    public double DiscountSize => TotalPrice - DiscountedPrice;
    public double DiscountedPrice => (FullPrice - (FullPrice * Discount)) * Quantity;

    public int Quantity { get; private set; }
    public int ProductId { get; private set; }
    public string? ProductName { get; set; }
    public int BasketId { get; private set; }

    public BasketItem(int productId, int quantity, double fullPrice, double discount, string productName)
    {
        ProductId = productId;
        FullPrice = fullPrice;
        Discount = discount;
        ProductName = productName;
        SetQuantity(quantity);
    }

    public void AddQuantity(int quantity)
    {
        if (quantity < int.MaxValue)
            Quantity += quantity;
    }
    public void DecreaseQuantity(int quantity)
    {
        if (quantity > 0)
            Quantity -= quantity;

    }

    public void SetQuantity(int quantity)
    {
        if (quantity < int.MaxValue)
            Quantity = quantity;
    }
}