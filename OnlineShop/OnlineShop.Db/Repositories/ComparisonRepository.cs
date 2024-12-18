using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public class ComparisonRepository : IComparisonRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<ComparisonRepository> _logger;

        public ComparisonRepository(DatabaseContext databaseContext, ILogger<ComparisonRepository> logger)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }

        public async Task<Comparison?> GetAsync(string userId)
        {
            try
            {
                var existingComparisons = await _databaseContext.Comparisons
                    .Include(x => x.Products)
                    .ThenInclude(x => x.Images)
                    .FirstOrDefaultAsync(x => x.UserId == userId);

                if (existingComparisons == null)
                {
                    return await AddAsync(userId);
                }

                return existingComparisons;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return await AddAsync(userId);
        }

        public async Task<bool> AddAsync(Comparison comparison)
        {
            try
            {
                var existingComparison = await GetAsync(comparison.UserId);

                if (existingComparison == null)
                {
                    await _databaseContext.Comparisons.AddAsync(comparison);
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

        public async Task<bool> UpdateAsync(Comparison comparison)
        {
            try
            {
                var existingComparison = await GetAsync(comparison.UserId);

                if (existingComparison != null)
                {
                    _databaseContext.Comparisons.Update(comparison);
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
                var existingComparison = await GetAsync(userId);

                _databaseContext.Comparisons.Remove(existingComparison);
                await _databaseContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return false;
        }

        public async Task<Comparison?> AddAsync(string userId)
        {
            try
            {
                var newComparison = new Comparison
                {
                    UserId = userId
                };

                await _databaseContext.Comparisons.AddAsync(newComparison);
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
