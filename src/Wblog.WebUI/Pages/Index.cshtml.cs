using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WBlog.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Net;
using Wblog.WebUI.Models;
using Wblog.WebUI.Servises;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Wblog.WebUI.Extensions;

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
        [BindProperty(SupportsGet = true)]
        public DateState DateSort { get; set; }

        public PageParametrs PageParametrs { get; set; } = new PageParametrs();
        public FiltredDataModel? PostsData { get; set; }
        private readonly IBlogClient _blogClient;

        public List<SelectListItem> ar { get; } = Enum.GetValues<DateState>().Select(e => new SelectListItem { Value = e.ToString(), Text = (e.GetAttribute<DisplayAttribute>())?.Name ?? e.ToString() }).ToList();

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
            PageParametrs.ItemPerPage = 1;

            int offset = (CurrentPage - 1) * PageParametrs.ItemPerPage;

            try
            {
                PostsData = await _blogClient.GetAsync<FiltredDataModel>($"/api/post?limit={PageParametrs.ItemPerPage}&offset={offset}&tag={Tag}&query={Serch}&State={DateSort}");
                System.Console.WriteLine($"/api/post?limit={PageParametrs.ItemPerPage}&offset={offset}&tag={Tag}&query={Serch}&State={DateSort}");
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