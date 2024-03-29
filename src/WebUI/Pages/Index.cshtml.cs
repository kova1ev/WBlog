﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WBlog.WebUI.Models;
using WBlog.WebUI.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WBlog.WebUI.Extensions;
using WBlog.WebUI.Utilities;

namespace WBlog.WebUI.Pages;

public class IndexModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? Tag { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }
    [BindProperty(Name = "p", SupportsGet = true)]
    public int CurrentPage { get; set; } = 1;
    [BindProperty(SupportsGet = true)]
    public DateState DateSort { get; set; }

    public PageParameters PageParameters { get; set; } = new PageParameters();
    public FilteredDataModel<ArticleIndexViewModel>? PostsData { get; set; }
    private readonly IBlogClient _blogClient;

    public List<SelectListItem> EnumSelectListItems { get; } = Enum.GetValues<DateState>().Select(e => new SelectListItem { Value = e.ToString(), Text = (e.GetAttribute<DisplayAttribute>())?.Name ?? e.ToString() }).ToList();

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, IBlogClient blogClient)
    {
        _logger = logger;
        _blogClient = blogClient;
    }

    public async Task<ActionResult> OnGetAsync()
    {
        PageParameters.CurrentPage = CurrentPage;
        PageParameters.ItemPerPage = 3;
        try
        {
            string url = UrlBuilder.Article.GetAllArticlesByParametr(PageParameters, DateSort, Tag, Search);
            PostsData = await _blogClient.GetAsync<FilteredDataModel<ArticleIndexViewModel>>(url);
            PageParameters.TotalItems = PostsData.TotalItems;
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
