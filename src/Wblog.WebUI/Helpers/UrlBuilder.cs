using System.Text;
using WBlog.Shared.Models;

namespace Wblog.WebUI.Helpers
{
    public static class UrlBuilder
    {
        public static class Article
        {
            private static string defaultRoute = "api/post/";
            public static string GetAllArticlesByParametr( PageParametrs param, DateState sort, string? tag = null, string? serchString = null)
            {
                int offset = (param.CurrentPage - 1) * param.ItemPerPage;
                StringBuilder urlstring = new StringBuilder();
                urlstring.Append(defaultRoute + $"?limit={param.ItemPerPage}&offset={offset}");
                if (!string.IsNullOrWhiteSpace(tag))
                {
                    urlstring.Append($"&tag={tag}");
                }
                if (!string.IsNullOrWhiteSpace(serchString))
                {
                    urlstring.Append($"&query={serchString}");
                }
                if (sort == DateState.DateAsc)
                {
                    urlstring.Append($"&State={sort}");
                }
                return urlstring.ToString();
            }
        }
    }
}