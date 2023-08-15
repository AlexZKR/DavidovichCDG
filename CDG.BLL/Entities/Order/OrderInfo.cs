#pragma warning disable CS8618 // Required by Entity Framework

using CDG.BLL.Entities.Enums;

namespace CDG.BLL.Entities.Order;

public class OrderInfo //Value object
{

    public OrderInfo()
    {
    }

    public OrderInfo(DateTime OrderDate)
    {
        this.OrderDate = OrderDate;
    }

    public DateTime OrderDate { get; private set; } = DateTime.Now;
}