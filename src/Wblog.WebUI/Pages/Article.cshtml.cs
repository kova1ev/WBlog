using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;
using WBlog.Application.Core.Dto;

namespace Wblog.WebUI.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly HttpClient httpClient;

        public PostDetailsDto? Post { get; set; }

        public ArticleModel(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ActionResult> OnGetAsync([FromRoute] string slug)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/post/{slug}");
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Post = JsonSerializer.Deserialize<PostDetailsDto>(jsonString, options);

            }
            catch (HttpRequestException ex)
            {
                //todo log
                if (ex.StatusCode == null)
                    return StatusCode(503);
                return StatusCode((int)ex.StatusCode);
            }
            return Page();
        }
    }
}
