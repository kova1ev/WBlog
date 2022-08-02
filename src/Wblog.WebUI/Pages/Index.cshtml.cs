using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WBlog.Shared.Models;
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
        public string? Tag { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Serch { get; set; }
        [BindProperty(Name = "p", SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public PageParametrs PageParametrs { get; set; } = new PageParametrs();

        public FiltredPostsModel? PostsData { get; set; }

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
            //TODO составлять строку uri и перевлдать в метод
            PageParametrs.CurrentPage = CurrentPage;
            PageParametrs.ItemPerPage = 10;

            int offset = (CurrentPage - 1) * PageParametrs.ItemPerPage;

            try
            {
                PostsData = await _blogClient.GetAsync<FiltredPostsModel>($"/api/post?limit={PageParametrs.ItemPerPage}&offset={offset}&tag={Tag}&query={Serch}");
                PageParametrs.TotalItems = PostsData.TotalItems;
            }
            catch (HttpRequestException ex)
            {
                //TODO log
                if (ex.StatusCode == null)
                    return StatusCode(503);
                return StatusCode((int)ex.StatusCode);
            }
            return Page();
        }

    }
}