using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<ProductsRepository> _logger;

        public OrdersRepository(DatabaseContext databaseContext, ILogger<ProductsRepository> logger)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            try
            {
                var existingOrders = await _databaseContext.Orders
                    .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                    .OrderBy(x => x.Number)
                    .ToListAsync();

                return existingOrders;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return [];
        }

        public async Task<Order?> GetAsync(string id)
        {
            try
            {
                var existingCart = await _databaseContext.Orders
                    .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (existingCart == null)
                {
                    return await AddAsync(id);
                }

                return existingCart;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return await AddAsync(id);
        }

        public async Task<bool> AddAsync(Order order)
        {
            try
            {
                await _databaseContext.Orders.AddAsync(order);
                await _databaseContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return false;
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            try
            {
                _databaseContext.Orders.Update(order);
                await _databaseContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return false;
        }

        private async Task<Order?> AddAsync(string id)
        {
            try
            {
                var newOrder = new Order();

                await _databaseContext.Orders.AddAsync(newOrder);
                await _databaseContext.SaveChangesAsync();

                return await GetAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }
    }
}
