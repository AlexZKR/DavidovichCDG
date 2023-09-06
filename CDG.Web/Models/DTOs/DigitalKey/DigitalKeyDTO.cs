namespace CDG.Web.Models.DigitalKey;

public class DigitalKeyDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";

    //enums
    public int? Tag { get; set; }
    public int? KeyCategoryId { get; set; }

    //$$
    public double FullPrice { get; set; } = 0;
    public double Discount { get; set; } = 0;
    public int Quantity { get; set; } = 0;
    public int Sold { get; set; }
    public string? PictureUri { get; set; } = "no_img";
    public double Rating { get; set; }
}
