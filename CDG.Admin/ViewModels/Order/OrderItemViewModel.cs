namespace CDG.Admin.ViewModels.Order;

public class OrderItemViewModel : BaseViewModel
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public double FullPrice { get; set; }
    public double TotalPrice => FullPrice * Units;
    public double DiscountedPrice { get; set; }
    public double Discount { get; set; }
    public int Units { get; set; }
    public string? AddInfo { get; set; }

}