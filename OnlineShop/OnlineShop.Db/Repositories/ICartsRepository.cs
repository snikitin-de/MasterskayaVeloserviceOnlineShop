using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public interface ICartsRepository
    {
        Task<Cart?> GetAsync(string userId);
        Task<bool> AddAsync(Cart cart);
        Task<bool> UpdateAsync(Cart cartDb);
        Task<bool> RemoveAsync(string userId);
    }
}
