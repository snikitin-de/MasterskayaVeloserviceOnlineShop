using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public class CartsRepository : ICartsRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<CartsRepository> _logger;

        public CartsRepository(DatabaseContext databaseContext, ILogger<CartsRepository> logger)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }

        public async Task<Cart?> GetAsync(string userId)
        {
            try
            {
                var existingCart = await _databaseContext.Carts
                    .Include(x => x.Items)
                    .ThenInclude(x => x.Product)
                    .FirstOrDefaultAsync(x => x.UserId == userId);

                if (existingCart == null)
                {
                    return await AddAsync(userId);
                }

                return existingCart;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return await AddAsync(userId);
        }

        public async Task<bool> AddAsync(Cart cart)
        {
            try
            {
                var existingCart = await GetAsync(cart.UserId);

                if (existingCart == null)
                {
                    await _databaseContext.Carts.AddAsync(cart);
                    await _databaseContext.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return false;
        }

        public async Task<bool> UpdateAsync(Cart cart)
        {
            try
            {
                var existingCart = await GetAsync(cart.UserId);

                if (existingCart != null)
                {
                    _databaseContext.Carts.Update(cart);
                    await _databaseContext.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return false;
        }

        public async Task<bool> RemoveAsync(string userId)
        {
            try
            {
                var cart = await GetAsync(userId);

                _databaseContext.CartItems.RemoveRange(cart.Items);
                await _databaseContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return false;
        }

        private async Task<Cart?> AddAsync(string userId)
        {
            try
            {
                var newCart = new Cart
                {
                    UserId = userId
                };

                await _databaseContext.Carts.AddAsync(newCart);
                await _databaseContext.SaveChangesAsync();

                return await GetAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }
    }
}
