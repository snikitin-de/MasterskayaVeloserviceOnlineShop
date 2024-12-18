namespace OnlineShopWebApp.Models
{
    public class ComparisonVM
    {
        public string? Id { get; set; }
        public string UserId { get; set; }
        public List<ProductVM> Products { get; set; }

        public ComparisonVM()
        {
            Products = [];
        }
    }
}
