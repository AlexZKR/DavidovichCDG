
using CDG.BLL.Entities.Order;

namespace CDG.BLL.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
    Task SendOrderAsync(List<OrderItem> items, string email, string subject = "Заказ CDG");
}
