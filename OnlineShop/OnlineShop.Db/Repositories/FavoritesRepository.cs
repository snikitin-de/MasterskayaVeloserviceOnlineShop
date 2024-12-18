using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public class FavoritesRepository : IFavoritesRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<FavoritesRepository> _logger;

        public FavoritesRepository(DatabaseContext databaseContext, ILogger<FavoritesRepository> logger)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }

        public async Task<Favorites?> GetAsync(string userId)
        {
            try
            {
                var existingFavorites = await _databaseContext.Favorites
                    .Include(x => x.Products)
                    .ThenInclude(x => x.Images)
                    .FirstOrDefaultAsync(x => x.UserId == userId);

                if (existingFavorites == null)
                {
                    return await AddAsync(userId);
                }

                return existingFavorites;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return await AddAsync(userId);
        }

        public async Task<bool> AddAsync(Favorites favorites)
        {
            try
            {
                var existingFavorites = await GetAsync(favorites.UserId);

                if (existingFavorites == null)
                {
                    await _databaseContext.Favorites.AddAsync(favorites);
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

        public async Task<bool> UpdateAsync(Favorites favorites)
        {
            try
            {
                var existingFavorites = await GetAsync(favorites.UserId);

                if (existingFavorites != null)
                {
                    _databaseContext.Favorites.Update(favorites);
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
                var existingFavorites = await GetAsync(userId);

                _databaseContext.Favorites.Remove(existingFavorites);
                await _databaseContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return false;
        }

        public async Task<Favorites?> AddAsync(string userId)
        {
            try
            {
                var newFavorites = new Favorites
                {
                    UserId = userId
                };

                await _databaseContext.Favorites.AddAsync(newFavorites);
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
