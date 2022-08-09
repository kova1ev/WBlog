using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Wblog.WebUI.Servises
{
    public class BlogClient : IBlogClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<AppSettings> _settings;

        public BlogClient(IOptions<AppSettings> options, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _settings = options;
            _httpClient.BaseAddress = new Uri(_settings.Value.BlogUrl);
        }
        //TODO передавать в метод uri builder вместо стоки
        //TODO Сдделать класс uri builder
        public async Task<T> GetAsync<T>(string urlParameters) where T : class
        {
            HttpResponseMessage response = await _httpClient.GetAsync(urlParameters);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            T resultData = JsonSerializer.Deserialize<T>(jsonString, options);
            return resultData;
        }
    }
}
