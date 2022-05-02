using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WBlog.Application.Core.Dto;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Net;

namespace Wblog.WebUI.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Tag { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Serch { get; set; }


        public PagedPosts? PostsData { get; set; }

        private readonly HttpClient client;
        public string? Message { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, HttpClient client)
        {
            _logger = logger;
            this.client = client;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            try
            {
                //PostsData = await client.GetFromJsonAsync<PagedPosts>("api/post");
                HttpResponseMessage response = await client.GetAsync($"/api/post?tag={Tag}&query={Serch}");


                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions();
                options.PropertyNameCaseInsensitive = true;

                PostsData = JsonSerializer.Deserialize<PagedPosts>(jsonString, options) ?? new PagedPosts();
                //Message = response.RequestMessage.RequestUri.AbsolutePath;

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