using CDG.Web.Models.Catalog;

namespace CDG.Web.Services;

public interface IRatingViewModelService
{
   RatingViewModel CreateViewModel(int rating);
}
