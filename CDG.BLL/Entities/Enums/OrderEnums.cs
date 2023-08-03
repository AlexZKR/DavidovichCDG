using System.ComponentModel.DataAnnotations;

namespace CDG.BLL.Entities.Enums;

public enum PaymentType
{
    [Display(Name = "Наличные")]
    Cash,
    [Display(Name = "Карта")]
    PaymentCard
};
