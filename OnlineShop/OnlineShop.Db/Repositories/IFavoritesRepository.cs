using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public interface IFavoritesRepository
    {
        Task<Favorites> GetAsync(string userId);
        Task<bool> AddAsync(Favorites item);
        Task<Favorites> AddAsync(string userId);
        Task<bool> UpdateAsync(Favorites item);
        Task<bool> RemoveAsync(string userId);
    }
}
