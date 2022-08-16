using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Wblog.WebUI.Servises
{
    public class BlogClient : IBlogClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<AppSettings> _appSettings;

        public BlogClient(IOptions<AppSettings> options, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _appSettings = options;
            _httpClient.BaseAddress = new Uri(_appSettings.Value.BaseAddress);
        }

        public async Task<T> GetAsync<T>(string urlString) where T : class
        {
            HttpResponseMessage response = await _httpClient.GetAsync(urlString);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            T resultData = JsonSerializer.Deserialize<T>(jsonString, options);
            return resultData;
        }
    }
}
