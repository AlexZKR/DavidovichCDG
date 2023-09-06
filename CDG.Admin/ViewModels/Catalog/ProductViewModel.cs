using System.ComponentModel.DataAnnotations;
using CDG.Admin.Models.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CDG.Admin.ViewModels.Catalog
{
    public class ProductViewModel : BaseViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(100, ErrorMessage = "До 100 символов")]
        public string? Name { get; set; }
        [Display(Name = "Описание")]
        [MaxLength(1500, ErrorMessage = "До 1500 символов")]
        public string? Description { get; set; }
        [Display(Name = "Количество страниц")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(0, int.MaxValue, ErrorMessage = "Не меньше нуля")]

        public string? TagDisplay { get; set; }

        [Display(Name = "Полная цена, руб.")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(0, int.MaxValue, ErrorMessage = "Не меньше нуля")]
        public double FullPrice { get; set; }
        [Display(Name = "Скидка, %.")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(0, int.MaxValue, ErrorMessage = "Не меньше нуля")]
        public double Discount { get; set; }
        [Display(Name = "Цена со скидкой, руб.")]
        public double DiscountedPrice => FullPrice - (FullPrice * Discount);
        [Display(Name = "Количество, шт.")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(0, double.MaxValue, ErrorMessage = "Не меньше нуля")]
        public int Quantity { get; set; }
        [Display(Name = "Продано, шт.")]
        [Range(0, double.MaxValue)]
        public int Sold { get; set; }
        [Display(Name = "Изображение")]
        public string? PictureUri { get; set; } = "no_img.jpg";
        [Display(Name = "Рейтинг")]
        public double Rating { get; set; }

        //selectLists
    [Display(Name = "Категория")]
    [Required(ErrorMessage = "Обязательное поле")]
    public int? KeyCategoryId { get; set; }
    [Display(Name = "Тэг")]
    [Required(ErrorMessage = "Обязательное поле")]
    public int? Tag { get; set; }

    //static data
    [Display(Name = "Тэг")]
    public List<SelectListItem>? Tags { get; set; }
    [Display(Name = "Категория")]
    public List<SelectListItem>? Categories { get; set; }
    }
}