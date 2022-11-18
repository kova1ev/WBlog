using WBlog.Core.Domain.Entity;

namespace WBlog.Admin.Models;

public class ArticleFullModel
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public string? Slug { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Content { get; set; }
    public bool IsPublished { get; set; }
    public IEnumerable<string> Tags { get; set; } = Enumerable.Empty<string>();

    public ArticleFullModel()
    {
    }

    public ArticleFullModel(Post post)
    {
        Id = post.Id;
        Slug = post.Slug;
        DateCreated = post.DateCreated;
        DateUpdated = post.DateUpdated;
        Title = post.Title;
        IsPublished = post.IsPublished;
        Description = post.Description;
        Content = post.Content;
        Tags = post.Tags.Select(t => t.Name);
    }
}