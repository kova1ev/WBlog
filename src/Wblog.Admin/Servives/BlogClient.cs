using System.Net;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WBlog.Shared.Models;

namespace Wblog.Admin.Servises
{
    public class BlogClient : IBlogClient
    {
        private readonly HttpClient _httpClient;

        public BlogClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException($"{nameof(httpClient)}");
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

        public async Task<bool> PostAsync<T>(string urlString, T entity) where T : class
        {
            var entityForPost = JsonSerializer.Serialize(entity);
            var requestContent = new StringContent(entityForPost, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(urlString, requestContent);
            var content = await response.Content.ReadAsStreamAsync();
            response.EnsureSuccessStatusCode();
            // if (!response.IsSuccessStatusCode)
            // {
            //     throw new ApplicationException($"{response.ReasonPhrase} {content}");
            // }

            var result = JsonSerializer.Deserialize<bool>(content);
            return result;
        }

        public async Task<bool> DeleteAsync(string urlString)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(urlString);

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            bool result = JsonSerializer.Deserialize<bool>(jsonString);

            return result;
        }

        public async Task<bool> PutAsync<T>(string urlString, T entity) where T : class
        {
            var entityForPut = JsonSerializer.Serialize(entity);
            var requestContent = new StringContent(entityForPut, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(urlString, requestContent);
            var content = await response.Content.ReadAsStringAsync();
            Handler(response);
            //response.EnsureSuccessStatusCode();
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

        private void Handler(HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.PropertyNameCaseInsensitive = true;
                Console.WriteLine(content);
                var details = JsonSerializer.Deserialize<ProblemDetails>(content, options);
                Console.WriteLine($"{details.Title} {details.Status} {details.Detail} ");

                throw new HttpRequestException(details.Detail, null, (HttpStatusCode)details.Status);
            }
        }
    }
}