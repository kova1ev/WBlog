﻿using WBlog.Core.Domain.Entity;

namespace WBlog.Admin.Models;

public class ArticleIndexModel
{
    public Guid Id { get; set; }
    public string? Slug { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public string? Title { get; set; }
    public bool IsPublished { get; set; }

    public ArticleIndexModel()
    {
    }

    public ArticleIndexModel(Post post)
    {
        Id = post.Id;
        Slug = post.Slug;
        DateCreated = post.DateCreated;
        DateUpdated = post.DateUpdated;
        Title = post.Title;
        IsPublished = post.IsPublished;
    }
}