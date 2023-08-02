using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CDG.BLL.Interfaces;

namespace CDG.BLL.Entities.Products
{
    public class KeyCategory : BaseEntity, IAggregateRoot
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Наименование")]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }
        [Display(Name = "Изображение")]
        public string PictureUri { get; set; } = SD.NO_PHOTO;
        [NotMapped]
        [Display(Name = "Изображение")]

        //nav
        public List<DigitalKey> Keys { get; set; } = new List<DigitalKey>();

    }
}