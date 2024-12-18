using OnlineShopWebApi.Models;

namespace OnlineShopWebApi.Services
{
    public interface IAccountService
    {
        Task<bool> HasAuthenticationPassedAsync(Authorization data);
        Task<bool> HasRegistrationPassedAsync(Registration data);
    }
}
