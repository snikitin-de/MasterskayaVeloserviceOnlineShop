namespace OnlineShop.Db.Models
{
    public class CartItem
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        public Product Product { get; set; }
        public Cart Cart { get; set; }
        public int Quantity { get; set; } = 1;

        public CartItem() { }

        public void Increase(Product product)
        {
            Quantity++;
        }

        public void Decrease(Product product)
        {
            Quantity--;
        }
    }
}
