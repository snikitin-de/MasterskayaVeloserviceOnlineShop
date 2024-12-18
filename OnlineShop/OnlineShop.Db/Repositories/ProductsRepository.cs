using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<ProductsRepository> _logger;

        public ProductsRepository(DatabaseContext databaseContext, ILogger<ProductsRepository> logger)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }

        public async Task<List<Service>> GetAllServicesAsync()
        {
            try
            {
                var services = await _databaseContext.Services.Include(x => x.Images).ToListAsync();

                return services?.ToList() ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return [];
        }

        public async Task<Service?> GetServiceAsync(string id)
        {
            try
            {
                var services = await GetAllServicesAsync();

                return services.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task AddServiceAsync(Service service)
        {
            try
            {
                await _databaseContext.Services.AddAsync(service);
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task UpdateServiceAsync(Service service)
        {
            if (service != null)
            {
                try
                {
                    var serviceDb = await _databaseContext.Services.FirstOrDefaultAsync(x => x.Id == service.Id);

                    if (serviceDb != null)
                    {
                        _databaseContext.Entry(serviceDb).CurrentValues.SetValues(service);
                        await _databaseContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }

        public async Task RemoveServiceAsync(string id)
        {
            if (id != null)
            {
                try
                {
                    var service = await GetServiceAsync(id);

                    if (service != null)
                    {
                        _databaseContext.Services.Remove(service);
                        await _databaseContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }

        public async Task<List<BikePart>> GetAllBikePartsAsync()
        {
            try
            {
                var bikeParts = await _databaseContext.BikeParts.Include(x => x.Images).ToListAsync();

                return bikeParts?.ToList() ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return [];
        }

        public async Task<BikePart?> GetBikePartAsync(string id)
        {
            try
            {
                var bikeParts = await GetAllBikePartsAsync();

                return bikeParts.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task AddBikePartAsync(BikePart bikePart)
        {
            try
            {
                await _databaseContext.BikeParts.AddAsync(bikePart);
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task UpdateBikePartAsync(BikePart bikePart)
        {
            if (bikePart != null)
            {
                try
                {
                    var bikePartDb = await _databaseContext.BikeParts.FirstOrDefaultAsync(x => x.Id == bikePart.Id);

                    if (bikePartDb != null)
                    {
                        _databaseContext.Entry(bikePartDb).CurrentValues.SetValues(bikePart);
                        await _databaseContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }

        public async Task RemoveBikePartAsync(string id)
        {
            if (id != null)
            {
                try
                {
                    var bikePart = await GetBikePartAsync(id);

                    if (bikePart != null)
                    {
                        _databaseContext.BikeParts.Remove(bikePart);
                        await _databaseContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }

        public async Task<Product?> GetProductAsync(string id)
        {
            try
            {
                return await _databaseContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }
    }
}
