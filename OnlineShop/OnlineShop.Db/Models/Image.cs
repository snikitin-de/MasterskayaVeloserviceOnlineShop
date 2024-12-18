namespace OnlineShop.Db.Models
{
    public class Image
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Path { get; set; }
        public string? ProductId { get; set; }
        public Product? Product { get; set; }
        public string? UserId { get; set; }

        public Image() { }
    }
}
