using Microsoft.AspNetCore.Mvc;
using WBlog.WebUI.Models;
using WBlog.WebUI.Servises;

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
        var result = await _blogClient.GetAsync<IEnumerable<PopularTagViewModel>>("/api/tag/popular");
        return View(result);
    }
}
