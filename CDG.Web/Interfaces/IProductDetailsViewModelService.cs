using CDG.Web.Models.Catalog;

namespace CDG.Web.Services;

public interface IProductDetailsViewModelService
{
    Task<ProductDetailsViewModel> CreateViewModel(int id, string username);
}
