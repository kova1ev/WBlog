﻿namespace WBlog.WebUI.Models;

public class ArticleIndexViewModel
{
    public Guid Id { get; set; }
    public string? Slug { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsPublished { get; set; }
}
