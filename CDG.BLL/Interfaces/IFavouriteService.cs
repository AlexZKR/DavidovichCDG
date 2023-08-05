using CDG.BLL.Entities.Products;

namespace CDG.BLL.Interfaces;

public interface IFavouriteService<T> where T : BaseProduct, IAggregateRoot
{
    // public Task<bool> CheckIfFavourite(string username, T entity);
    public bool CheckIfFavourite(string username, T entity);
    public bool CheckIfFavourite(string username, int id);
    Task<bool> RemoveFromFavourites(string username, string Id);
    Task<bool> RemoveFromFavourites(string username, T entity);
    public Task<List<T>> GetFavouritesForUser(string username);
    public Task<bool> UpdateFavourite(string username, string Id);
}