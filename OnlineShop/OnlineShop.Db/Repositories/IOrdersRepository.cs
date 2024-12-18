using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public interface IOrdersRepository
    {
        Task<List<Order>> GetAllAsync();
        Task<Order?> GetAsync(string id);
        Task<bool> AddAsync(Order order);
        Task<bool> UpdateAsync(Order order);
    }
}
