#pragma warning disable CS8618 // Required by Entity Framework

using CDG.BLL.Entities.Enums;

namespace CDG.BLL.Entities.Order;

public class OrderInfo //Value object
{

    public OrderInfo()
    {
    }

    public OrderInfo(DateTime OrderDate,
                     PaymentType PaymentType,
                     string? OrderComment)
    {
        this.OrderDate = OrderDate;
        this.PaymentType = PaymentType;
        this.OrderComment = OrderComment;
    }

    public DateTime OrderDate { get; private set; } = DateTime.Now;
    public PaymentType PaymentType { get; set; }
    public string? OrderComment { get; set; }
}