using CDG.BLL.Interfaces;

namespace CDG.BLL.Entities.BasketAggregate;

public class Basket : BaseEntity, IAggregateRoot
{
    public string BuyerId { get; private set; }
    private readonly List<BasketItem> items = new List<BasketItem>();
    public IReadOnlyCollection<BasketItem> Items => items.AsReadOnly();

    public int TotalItems => items.Sum(i => i.Quantity);
    public double TotalDiscountSize => items.Sum(i => i.DiscountSize);
    public double TotalDiscountedPrice => items.Sum(i => i.DiscountedPrice);


    public Basket(string buyerId)
    {
        BuyerId = buyerId;
    }

    public void AddItem(int itemId, double fullPrice, double discount, string productName, int quantity = 1)
    {
        // if (!Items.Any(i => i.ProductId == itemId))
        // {
            items.Add(new BasketItem(itemId, quantity, fullPrice, discount, productName));
            return;
        // }
        // var existingItem = Items.First(i => i.ProductId == itemId);
        // existingItem.AddQuantity(quantity);
    }

    public void RemoveEmptyItems()
    {
        items.RemoveAll(i => i.Quantity == 0);
    }

}