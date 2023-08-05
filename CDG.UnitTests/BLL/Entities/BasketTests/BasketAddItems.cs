using CDG.BLL.Entities.BasketAggregate;

namespace CDG.UnitTests.BLL.Entities.BasketTests;

public class BasketAddItem
{
    private readonly int testCatalogItemId = 123;
    private readonly double testUnitPrice = 10;
    private readonly double testDiscount = 0.1;
    private readonly int testQuantity = 2;
    private readonly string testProductName = "Test prodName";
    private readonly string buyerId = "Test buyerId";

    [Fact]
    public void AddItemWithNoItem()
    {
        Basket basket = new Basket(buyerId);

        basket.AddItem(testCatalogItemId, testUnitPrice, testDiscount, testProductName, testQuantity);

        Assert.Equal(testQuantity, basket.TotalItems);
    }

    [Fact]
    public void AddEqualItem()
    {
        Basket basket = new Basket(buyerId);
        basket.AddItem(testCatalogItemId, testUnitPrice, testDiscount, testProductName, testQuantity);
        basket.AddItem(testCatalogItemId, testUnitPrice, testDiscount, testProductName, testQuantity);

        var result = basket.TotalItems;

        Assert.Equal(testQuantity * 2, result);
    }
}