using System.Text;
using Wblog.WebUI.Models;

namespace Wblog.WebUI.Helpers
{
    public static class UrlBuilder
    {
        public static class Article
        {
            public static string GetAllArticlesByParametr(string urlRoute, PageParametrs param, DateState sort, string? tag = null, string? serchString = null)
            {
                int offset = (param.CurrentPage - 1) * param.ItemPerPage;
                StringBuilder urlstring = new StringBuilder();
                urlstring.Append(urlRoute + $"?limit={param.ItemPerPage}&offset={offset}");
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
            public static string GetAllTagsByParametr(string urlRoute, PageParametrs param, string? serchString)
            {
                int offset = (param.CurrentPage - 1) * param.ItemPerPage;
                StringBuilder urlstring = new StringBuilder();
                urlstring.Append(urlRoute + $"?limit={param.ItemPerPage}&offset={offset}");
                if (!string.IsNullOrWhiteSpace(serchString))
                {
                    urlstring.Append($"&query={serchString}");
                }
                return urlstring.ToString();
            }

        }

    }
}