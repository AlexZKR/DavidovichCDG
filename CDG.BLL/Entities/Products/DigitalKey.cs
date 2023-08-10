using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDG.BLL.Entities.Enums;
using CDG.BLL.Interfaces;

namespace CDG.BLL.Entities.Products;

public class DigitalKey : BaseProduct, IAggregateRoot
{
    //enums

    [DataType(DataType.Text)]
    [StringLength(500)]
    public string Key { get; set; }

    [DataType(DataType.Text)]
    [StringLength(500)]
    [Display(Name = "Тэг")]
    public Tag Tag { get; set; } = Tag.None;

    //nav
    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }
    [Display(Name = "Категория")]
    public KeyCategory KeyCategory { get; set; } = null!;

    public DigitalKey(string key)
    {
        Key = key;
    }
}