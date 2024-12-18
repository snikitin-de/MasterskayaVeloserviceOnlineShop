namespace OnlineShopWebApp.Models
{
    public class OrderItemVM
    {
        public string? Id { get; set; }
        public ProductVM Item { get; set; }
        public int Quantity { get; set; }
        public decimal Sum
        {
            get => Item.Price * Quantity;
        }
    }
}
