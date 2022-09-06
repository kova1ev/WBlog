using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using WBlog.Shared.Models;

namespace Wblog.WebUI.Servises
{
    public class BlogClient : IBlogClient
    {
        private readonly HttpClient _httpClient;

        public BlogClient(IOptions<SiteOptions> options, HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException($"{nameof(httpClient)}");
            _httpClient.BaseAddress = new Uri(options.Value.BaseAddress ?? throw new InvalidOperationException());
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