using System.ComponentModel.DataAnnotations;

namespace CDG.Admin.Models.Product;


public enum Tag
{
    [Display(Name = "Нет")]
    None,
    [Display(Name = "Классика")]
    Classic,
    [Display(Name = "Бестселлер")]
    Bestseller
};