namespace OnlineShopWebApp.Models
{
    public class FavoritesVM
    {
        public string? Id { get; set; }
        public string UserId { get; set; }
        public List<ProductVM> Products { get; set; }

        public FavoritesVM()
        {
            Products = [];
        }
    }
}
