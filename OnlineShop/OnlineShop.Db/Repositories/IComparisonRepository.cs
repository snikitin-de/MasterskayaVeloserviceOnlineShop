using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public interface IComparisonRepository
    {
        Task<Comparison> GetAsync(string userId);
        Task<bool> AddAsync(Comparison item);
        Task<Comparison> AddAsync(string userId);
        Task<bool> UpdateAsync(Comparison item);
        Task<bool> RemoveAsync(string userId);
    }
}
