using CDG.Web.Models.Order;

namespace CDG.Web.Interfaces
{
    public interface ICheckOutViewModelService
    {
        Task<CheckOutViewModel> CreateCheckOutVMAsync(string username);
    }
}