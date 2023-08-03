using System.ComponentModel.DataAnnotations;
using CDG.BLL.Interfaces;

namespace CDG.BLL.Entities.Products;

public class UserFavourites : IAggregateRoot
{
    [Key]
    public string? Username { get; set; }
    public string Favourites { get; set; } = String.Empty;

    public List<string> GetFavs()
    {
        if (Favourites != null)
            return Favourites.Split(',').ToList();
        else
            return new List<string>();
    }
}