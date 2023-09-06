namespace CDG.Admin.Models.Product;

public class ProductDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? Tag { get; set; }
    public double FullPrice { get; set; }
    public double Discount { get; set; }
    public int Quantity { get; set; }
    public int Sold { get; set; }
    public string? PictureUri { get; set; }
    public double Rating { get; set; }
    public int? KeyCategoryId { get; set; }

}