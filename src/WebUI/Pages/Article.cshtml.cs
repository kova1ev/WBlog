using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WBlog.WebUI.Services;
using WBlog.WebUI.Models;

namespace WBlog.WebUI.Pages;

public class ArticleModel : PageModel
{
    private readonly IBlogClient _blogClient;

    public ArticleFullViewModel? Post { get; set; }

    public ArticleModel(IBlogClient blogClient)
    {
        this._blogClient = blogClient;
    }
    public async Task<ActionResult> OnGetAsync([FromRoute] string slug)
    {
        try
        {
            Post = await _blogClient.GetAsync<ArticleFullViewModel>($"api/post/{slug}");
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

