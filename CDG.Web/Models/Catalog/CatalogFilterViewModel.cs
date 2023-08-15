using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CDG.Web.Models.Catalog;

public class CatalogFilterViewModel
{
    public string? SearchQuery { get; set; }

    //filters
    public int? keyCategory { get; set; }


    [Display(Name = "Категория")]
    public List<SelectListItem>? KeyCategorys { get; set; }
}