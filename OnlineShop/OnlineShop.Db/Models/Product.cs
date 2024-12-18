namespace OnlineShop.Db.Models
{
    public class Product
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public bool IsComparable { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<Comparison> Comparison { get; set; }
        public List<Favorites> Favorites { get; set; }
        public List<Image> Images { get; set; } = [];
    }
}

