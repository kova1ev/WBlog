using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WBlog.Shared.Models;
using WBlog.WebUI.Servises;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WBlog.WebUI.Extensions;
using WBlog.WebUI.Utilities;

namespace WBlog.WebUI.Pages;

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
    public FiltredDataModel<PostIndexModel>? PostsData { get; set; }
    private readonly IBlogClient _blogClient;

    public List<SelectListItem> EnumSelectListItems { get; } = Enum.GetValues<DateState>().Select(e => new SelectListItem { Value = e.ToString(), Text = (e.GetAttribute<DisplayAttribute>())?.Name ?? e.ToString() }).ToList();

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, IBlogClient blogClient)
    {
        _logger = logger;
        _blogClient = blogClient;
    }

    public async Task<ActionResult> OnGetAsync()
    {
        PageParametrs.CurrentPage = CurrentPage;
        PageParametrs.ItemPerPage = 3;
        try
        {
            string url = UrlBuilder.Article.GetAllArticlesByParametr(PageParametrs, DateSort, Tag, Serch);
            PostsData = await _blogClient.GetAsync<FiltredDataModel<PostIndexModel>>(url);
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
