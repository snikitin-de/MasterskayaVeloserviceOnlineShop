using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace OnlineShop.SecretManager.Services
{
    public class CloudRuSecretManager : ISecretManager
    {
        private DateTime tokenCreatedDate;
        private readonly HttpClient _httpClient;
        private readonly string _authUrl = "https://iam.api.cloud.ru/api/v1/auth/token";
        private readonly string _secretsBaseUrl = "https://secretmanager.api.cloud.ru/v1/secrets/{0}/versions/{1}/payload";

        public CloudRuSecretManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AuthorizeAsync(string keyId, string secret)
        {
            var currentDate = DateTime.Now;
            var duration = currentDate - tokenCreatedDate;

            if (duration <= TimeSpan.FromMinutes(59))
            {
                return;
            }

            var uriBuilder = new UriBuilder(_authUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            query["keyId"] = keyId;
            query["secret"] = secret;

            uriBuilder.Query = query.ToString();

            using var request = new HttpRequestMessage(HttpMethod.Post, uriBuilder.ToString());

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (content != null)
            {
                var jsonResponse = JsonConvert.DeserializeObject<TokenJsonResponse>(content);

                if (jsonResponse?.AccessToken != null)
                {
                    tokenCreatedDate = DateTime.Now;

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jsonResponse?.AccessToken);
                }
            }
        }

        public async Task<Dictionary<string, string>> GetAsync(string secretId, string version)
        {
            var secretsUrl = string.Format(_secretsBaseUrl, secretId, version);

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, secretsUrl);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (content != null)
            {
                var jsonResponse = JsonConvert.DeserializeObject<SecretJsonResponse>(content);

                if (jsonResponse?.Data != null)
                {
                    var decodedData = Convert.FromBase64String(jsonResponse.Data);
                    var decodedString = Encoding.UTF8.GetString(decodedData);

                    return JsonConvert.DeserializeObject<Dictionary<string, string>>(decodedString) ?? [];
                }
            }

            return [];
        }

        private class TokenJsonResponse
        {
            [JsonProperty("access_token")]
            public string? AccessToken { get; set; }
        }

        private class SecretJsonResponse
        {
            [JsonProperty("data")]
            public string Data { get; set; }
        }
    }
}
