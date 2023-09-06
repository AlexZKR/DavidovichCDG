namespace CDG.Admin.Models.Order
{
    public class BuyerDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int Discount { get; set; }
        public string? Email { get; set; }
        public int UnproccessedOrdersCount { get; set; }

        //orders are queried separately by buyers id
        //public List<OrderDTO> Orders { get; set; } = new List<OrderDTO>();
    }
}