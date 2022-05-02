using Microsoft.AspNetCore.Mvc;
using WBlog.Application.Core.Dto;
using System.Text.Json;

namespace Wblog.WebUI.Components
{
    public class TagsCloud : ViewComponent
    {
        private readonly HttpClient httpClient;
        public TagsCloud(HttpClient client)
        {
            this.httpClient = client;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await httpClient.GetAsync("/api/tag/popular");
            response.EnsureSuccessStatusCode();
            string jsonString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var result = JsonSerializer.Deserialize<IEnumerable<PopularTagDto>>(jsonString, options);
            return View(result);
        }
    }
}