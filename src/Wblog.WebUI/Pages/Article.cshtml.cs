using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;
using Wblog.WebUI.Servises;
using WBlog.Shared.Dto;

namespace Wblog.WebUI.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IBlogClient _blogCliet;

        public PostDetailsDto? Post { get; set; }

        public ArticleModel(IBlogClient blogCliet)
        {
           this._blogCliet = blogCliet;
        }
        public async Task<ActionResult> OnGetAsync([FromRoute] string slug)
        {
            try
            {
                Post = await _blogCliet.GetAsync<PostDetailsDto>($"api/post/{slug}");
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
