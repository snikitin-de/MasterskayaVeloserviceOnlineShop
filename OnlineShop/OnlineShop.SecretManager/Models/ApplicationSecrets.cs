namespace OnlineShop.SecretManager.Models
{
    public class ApplicationSecrets
    {
        public string? ConnectionString { get; set; }
        public string? JwtKey { get; set; }
        public string? JwtIssuer { get; set; }
        public string? JwtAudience { get; set; }
        public string? DefaultAdminEmail { get; set; }
        public string? DefaultAdminPassword { get; set; }
    }
}
