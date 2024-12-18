namespace OnlineShop.Db.Models
{
    public class Cart
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public List<CartItem> Items { get; set; } = [];

        public Cart() { }

        public void Increase(CartItem item)
        {
            item.Increase(item.Product);
        }

        public void Decrease(CartItem item)
        {
            if (item.Quantity > 1)
            {
                item.Decrease(item.Product);
                return;
            }

            Items.Remove(item);
        }
    }
}
