namespace OnlineShop.Db.Models
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Number { get; }
        public string? UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Middlename { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string? Comment { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Created;
        public List<OrderItem> OrderItems { get; set; } = [];

        public Order() { }
    }
}
