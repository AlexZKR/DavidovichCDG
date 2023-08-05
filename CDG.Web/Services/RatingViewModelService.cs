using CDG.Web.Models.Catalog;

namespace CDG.Web.Services;

public class RatingViewModelService : IRatingViewModelService
{
    public RatingViewModel CreateViewModel(int rating)
    {
        return new RatingViewModel
        {
            Rating = rating,
        };
    }
}