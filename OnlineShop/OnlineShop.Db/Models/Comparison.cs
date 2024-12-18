namespace OnlineShop.Db.Models
{
    public class Comparison
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public List<Product> Products { get; set; } = [];

        public Comparison() { }
    }
}
