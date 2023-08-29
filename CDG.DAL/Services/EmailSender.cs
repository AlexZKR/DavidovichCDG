
using System.Net;
using System.Net.Mail;
using CDG.BLL.Entities.Order;
using CDG.BLL.Interfaces;

namespace CDG.DAL.Services;

// This class is used by the application to send email for account confirmation and password reset.
// For more details see https://go.microsoft.com/fwlink/?LinkID=532713
public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        // TODO: Wire this up to actual email sending logic via SendGrid, local SMTP, etc.
        return Task.CompletedTask;
    }

    public Task SendOrderAsync(List<OrderItem> items, string email, string subject = "Заказ CDG")
    {
        var mail = "alexchatscammer12345@gmail.com";
        var pw = "yjfphntsnovufhkn";

        var client = new SmtpClient()
        {
            Port = 587,
            Host = "smtp.gmail.com", //or another email sender provider
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(mail, pw)
        };
        string message = string.Empty;
        foreach (var item in items)
        {
            message += item.ProductName + " : " + item.Key + "\n";
        }


        return client.SendMailAsync(new MailMessage(from: mail, to: email, subject: subject, body: message));
    }

}
