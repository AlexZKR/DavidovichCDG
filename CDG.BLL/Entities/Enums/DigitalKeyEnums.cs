using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CDG.BLL.Entities.Enums;

public enum Platform
{
    [Display(Name = "Все")]
    All,
    [Display(Name = "ОС \"Windows\"")]
    Windows,
    [Display(Name = "ОС \"Linux\"")]
    Linux,
    [Display(Name = "ОС \"Mac OS\"")]
    Mac,
    [Display(Name = "Steam")]
    Steam,
};

public enum Language
{
    [Display(Name = "Все")]
    All,
    [Display(Name = "Русский")]
    Russian,
    [Display(Name = "Белорусский")]
    Belarusian,
    [Display(Name = "Английский")]
    English
};

public enum Tag
{
    None,
    Classic,
    Bestseller
};