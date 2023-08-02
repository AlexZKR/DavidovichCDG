using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDG.BLL.Entities;
using CDG.BLL.Interfaces;

namespace CDG.BLL.Entities.Products;

public abstract class BaseProduct : BaseEntity, IAggregateRoot
{
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Наименование товара")]
    public string Name { get; set; } = "";

    [DataType(DataType.MultilineText)]
    [Display(Name = "Описание товара")]
    public string Description { get; set; } = "";
    [NotMapped]
    public bool IsFavourite { get; set; }

    //$$
    [Display(Name = "Полная стоимость товара, руб.")]
    [Range(0, int.MaxValue)]
    public double FullPrice { get; set; } = 0;
    [Range(0, 1)]
    [Display(Name = "Скидка")]
    public double Discount { get; set; } = 0;
    [NotMapped]
    [Display(Name = "Цена со скидкой")]
    public double DiscountedPrice => FullPrice - (FullPrice * Discount);

    [Display(Name = "В наличии")]
    public int Quantity { get; set; } = 0;

    [Display(Name = "Продано, шт.")]
    [Range(0, int.MaxValue)]
    public int Sold { get; set; }

    [Display(Name = "Изображение")]
    public string PictureUri { get; set; } = SD.NO_PHOTO;

    //av. rating for sorting and displaying
    [Display(Name = "Средний рейтинг")]
    [Range(0,5)]
    public double Rating { get; set; }
}