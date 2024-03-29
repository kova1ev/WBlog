﻿namespace WBlog.WebUI.Models;

public class ArticleFullViewModel
{
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public string? Slug { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Content { get; set; }
    public bool IsPublished { get; set; }
    public IEnumerable<string> Tags { get; set; } = Enumerable.Empty<string>();
}
