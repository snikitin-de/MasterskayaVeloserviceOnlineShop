namespace OnlineShop.Db.Models
{
    public class OrderItem
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        public Product Product { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; } = 1;

        public OrderItem() { }
    }
}
