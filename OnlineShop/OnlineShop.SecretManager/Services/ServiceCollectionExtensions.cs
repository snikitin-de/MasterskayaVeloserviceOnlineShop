using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.SecretManager.Models;

namespace OnlineShop.SecretManager.Services
{
    public static class ServiceCollectionExtensions
    {
        public static async Task<ApplicationSecrets> GetApplicationSecretsAsync(this IServiceCollection services, IConfiguration configuration)
        {
            var cloudRuKeyId = configuration["CLOUD_RU_KEY_ID"];
            var cloudRuSecret = configuration["CLOUD_RU_SECRET"];
            var dbSettingsSecretId = configuration["DATABASE_SETTINGS_SECRET_ID"];
            var dbSettingsSecretVersion = configuration["DATABASE_SETTINGS_SECRET_VERSION"];
            var webApiSecretId = configuration["WEBAPI_SECRET_ID"];
            var webApiSecretVersion = configuration["WEBAPI_SECRET_VERSION"];
            var appSecretId = configuration["APP_SECRET_ID"];
            var appSecretVersion = configuration["APP_SECRET_VERSION"];

            var httpClient = new HttpClient();
            var secretManager = new CloudRuSecretManager(httpClient);
            await secretManager.AuthorizeAsync(cloudRuKeyId, cloudRuSecret);

            var dbKeys = await secretManager.GetAsync(dbSettingsSecretId, dbSettingsSecretVersion);
            var webApiKeys = await secretManager.GetAsync(webApiSecretId, webApiSecretVersion);
            var appKeys = await secretManager.GetAsync(appSecretId, appSecretVersion);

            services.AddScoped<ISecretManager>(provider => secretManager);

            dbKeys.TryGetValue("ConnectionString", out var connectionString);
            webApiKeys.TryGetValue("JwtKey", out var jwtKey);
            webApiKeys.TryGetValue("JwtIssuer", out var jwtIssuer);
            webApiKeys.TryGetValue("JwtAudience", out var jwtAudience);
            appKeys.TryGetValue("DefaultAdminEmail", out var defaultAdminEmail);
            appKeys.TryGetValue("DefaultAdminPassword", out var defaultAdminPassword);

            return new ApplicationSecrets
            {
                ConnectionString = connectionString,
                JwtKey = jwtKey,
                JwtIssuer = jwtIssuer,
                JwtAudience = jwtAudience,
                DefaultAdminEmail = defaultAdminEmail,
                DefaultAdminPassword = defaultAdminPassword
            };
        }
    }
}
