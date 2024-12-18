using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public interface IProductsRepository
    {
        Task<List<Service>> GetAllServicesAsync();
        Task<Service?> GetServiceAsync(string id);
        Task AddServiceAsync(Service service);
        Task UpdateServiceAsync(Service service);
        Task RemoveServiceAsync(string id);

        Task<List<BikePart>> GetAllBikePartsAsync();
        Task<BikePart?> GetBikePartAsync(string id);
        Task AddBikePartAsync(BikePart bikePart);
        Task UpdateBikePartAsync(BikePart bikePart);
        Task RemoveBikePartAsync(string id);

        Task<Product?> GetProductAsync(string id);
    }
}
