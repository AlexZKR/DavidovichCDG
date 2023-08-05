using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CDG.Web.Models.Catalog;

public class CatalogFilterViewModel
{
    public string? SearchQuery { get; set; }

    //filters
    public int? cover { get; set; }
    public int? genre { get; set; }
    public int? lang { get; set; }
    public int? keyCategory { get; set; }

    //static data
    [Display(Name = "Жанр")]
    public List<SelectListItem>? Genres { get; set; }
    [Display(Name = "Язык")]
    public List<SelectListItem>? Languages { get; set; }
    [Display(Name = "Обложка")]
    public List<SelectListItem>? Covers { get; set; }
    [Display(Name = "Автор")]
    public List<SelectListItem>? KeyCategorys { get; set; }
}