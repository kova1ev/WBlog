using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WBlog.WebUI.Servises;
using WBlog.WebUI.Models;

namespace WBlog.WebUI.Pages;

public class ArticleModel : PageModel
{
    private readonly IBlogClient _blogCliet;

    public ArticleFullViewModel? Post { get; set; }

    public ArticleModel(IBlogClient blogCliet)
    {
        this._blogCliet = blogCliet;
    }
    public async Task<ActionResult> OnGetAsync([FromRoute] string slug)
    {
        try
        {
            Post = await _blogCliet.GetAsync<ArticleFullViewModel>($"api/post/{slug}");
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
