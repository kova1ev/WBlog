using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using WBlog.Shared.Models;

namespace Wblog.WebUI.Servises
{
    public class BlogClient : IBlogClient
    {
        private readonly HttpClient _httpClient;

        public BlogClient(IOptions<AppSettings> options, HttpClient httpClient)
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

        public async Task<bool> DeleteAsync(string urlString)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(urlString);

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            bool result = JsonSerializer.Deserialize<bool>(jsonString);

            return result;
        }

        public async Task<bool> PutAsync(string urlString, TagModel entity)
        {
            var tag = JsonSerializer.Serialize(entity);
            var requestContent = new StringContent(tag, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(urlString, requestContent);
            var content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            // if (!response.IsSuccessStatusCode)
            // {
            //     throw new ApplicationException($"{response.ReasonPhrase} {content}");
            // }

            var jsonString = await response.Content.ReadAsStringAsync();
            bool result = JsonSerializer.Deserialize<bool>(jsonString);

            return result;
        }

        public async Task<bool> PublishAsync(string urlString)
        {
            HttpResponseMessage response = await _httpClient.PutAsync(urlString, null);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStreamAsync();
            bool result = JsonSerializer.Deserialize<bool>(jsonString);
            return result;
        }
    }
}