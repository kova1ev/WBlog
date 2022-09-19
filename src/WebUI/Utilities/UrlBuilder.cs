using System.Text;
using WBlog.WebUI.Models;

namespace WBlog.WebUI.Utilities;

public static class UrlBuilder
{
    public static class Article
    {
        private static string defaultRoute = "api/post/getpublished";
        public static string GetAllArticlesByParametr(PageParametrs param, DateState sort, string? tag = null, string? serchString = null)
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

    public static class Tag
    {
        private static string defaultRoute = "api/tag/";
        public static string GetAllTagsByParametr(PageParametrs param, string? serchString)
        {
            int offset = (param.CurrentPage - 1) * param.ItemPerPage;
            StringBuilder urlstring = new StringBuilder();
            urlstring.Append(defaultRoute + $"?limit={param.ItemPerPage}&offset={offset}");
            if (!string.IsNullOrWhiteSpace(serchString))
            {
                urlstring.Append($"&query={serchString}");
            }
            return urlstring.ToString();
        }

    }
}
