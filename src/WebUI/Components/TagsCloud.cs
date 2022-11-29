using Microsoft.AspNetCore.Mvc;
using WBlog.WebUI.Models;
using WBlog.WebUI.Services;

namespace WBlog.WebUI.Components;

public class TagsCloud : ViewComponent
{
    private readonly IBlogClient _blogClient;
    public TagsCloud(IBlogClient blogClient)
    {
        this._blogClient = blogClient;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        try
        {
            var result = await _blogClient.GetAsync<IEnumerable<PopularTagViewModel>>("/api/tag/popular");
            return View(result);
        }
        catch (HttpRequestException ex)
        {
            //TODO log
            ViewBag.StatusCode = ex.StatusCode;
            if (ex.StatusCode == null)
                ViewBag.StatusCode = 503;
            ViewBag.Message = ex.Message;
            return View(Enumerable.Empty<PopularTagViewModel>());
        }
    }
}
