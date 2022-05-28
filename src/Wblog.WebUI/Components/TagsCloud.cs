using Microsoft.AspNetCore.Mvc;
using WBlog.Shared.Models;
using System.Text.Json;
using Wblog.WebUI.Servises;

namespace Wblog.WebUI.Components
{
    public class TagsCloud : ViewComponent
    {
        private readonly IBlogClient _blogClient;
        public TagsCloud(IBlogClient blogClient)
        {
            this._blogClient = blogClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _blogClient.GetAsync<IEnumerable<PopularTagModel>>("/api/tag/popular");
            return View(result);
        }
    }
}