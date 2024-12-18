namespace OnlineShopWebApp.Models
{
    public class CartVM
    {
        public string? Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemVM> Items { get; set; } = [];
        public decimal Sum
        {
            get => Items.Sum(x => x.Sum);
        }

        public decimal Quantity
        {
            get => Items.Sum(x => x.Quantity);
        }

        public CartVM() { }
    }
}
