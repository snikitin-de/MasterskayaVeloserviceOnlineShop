namespace OnlineShop.SecretManager.Services
{
    public interface ISecretManager
    {
        Task AuthorizeAsync(string keyId, string secret);
        Task<Dictionary<string, string>> GetAsync(string secretId, string version);
    }
}
