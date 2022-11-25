using System.Text;
using WBlog.WebUI.Models;

namespace WBlog.WebUI.Utilities;

public static class UrlBuilder
{
    public static class Article
    {
        private static string defaultRoute = "api/post/getpublished";
        public static string GetAllArticlesByParametr(PageParameters param, DateState sort, string? tag = null, string? searchString = null)
        {
            int offset = (param.CurrentPage - 1) * param.ItemPerPage;
            StringBuilder urlString = new StringBuilder();
            urlString.Append(defaultRoute + $"?limit={param.ItemPerPage}&offset={offset}");
            if (!string.IsNullOrWhiteSpace(tag))
            {
                urlString.Append($"&tag={tag}");
            }
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                urlString.Append($"&query={searchString}");
            }
            if (sort == DateState.DateAsc)
            {
                urlString.Append($"&State={sort}");
            }
            return urlString.ToString();
        }
    }

    public static class Tag
    {
        private static string defaultRoute = "api/tag/";
        public static string GetAllTagsByParametr(PageParameters param, string? searchString)
        {
            int offset = (param.CurrentPage - 1) * param.ItemPerPage;
            StringBuilder urlString = new StringBuilder();
            urlString.Append(defaultRoute + $"?limit={param.ItemPerPage}&offset={offset}");
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                urlString.Append($"&query={searchString}");
            }
            return urlString.ToString();
        }

    }
}
