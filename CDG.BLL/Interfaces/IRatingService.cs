using CDG.BLL.Entities.Order;

namespace CDG.BLL.Interfaces;

public interface IRatingService
{
    Task<int> SetRating(string username, int productId, int rating);
    Task<int> GetRating(string username, int productId);
    Task<double> UpdateProductAverageRating(int productId);
}