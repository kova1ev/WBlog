using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WBlog.Application.Core.Dto;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Net;
using Wblog.WebUI.Models;
using Wblog.WebUI.Servises;

namespace Wblog.WebUI.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Tag { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Serch { get; set; }

        [BindProperty(Name = "p", SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public PageParametrs PageParametrs { get; set; }


        public FiltredPostsDto? PostsData { get; set; }

        private readonly IBlogClient _blogClient;
        public string? Message { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, IBlogClient blogClient)
        {
            _logger = logger;
            _blogClient = blogClient;
        }

        public async Task<ActionResult> OnGetAsync()
        {

            //todo сделать в отдельном методе дисериализацию
            // составлять строку uri и перевлдать в метод
            string RequestUri;
            PageParametrs = new PageParametrs()
            {
                CurrentPage = CurrentPage,
                ItemPerPage = 10,
            };

            int limit = PageParametrs.ItemPerPage;
            int offset = (CurrentPage - 1) * limit;//PageParametrs.ItemPerPage;
            try
            {
                PostsData = await _blogClient.GetAsync<FiltredPostsDto>($"/api/post?limit={limit}&offset={offset}&tag={Tag}&query={Serch}");
                PageParametrs.TotalItems = PostsData.TotalItems;
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