using System.ComponentModel.DataAnnotations;
using WBlog.Core.Domain.Entity;
using WBlog.Shared.Attribute;

namespace WBlog.Admin.Models;

public class ArticleEditModel
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(250, MinimumLength = 5,
        ErrorMessage = $"{nameof(Title)} must contain between 5 and 250 characters")]
    public string? Title { get; set; }

    [Required]
    [RegularExpression(@"^(\w[A-Za-z])[A-Za-z0-9\-\s]+$",
        ErrorMessage =
            $"{nameof(Slug)} must contain only letters of the Latin alphabet and may contain numbers, dashes and spaces")]
    public string? Slug { get; set; }

    [Required]
    [StringLength(1000, MinimumLength = 5,
        ErrorMessage = $"{nameof(Description)} must contain between 5 and 1000 characters")]
    public string? Description { get; set; }

    [Required]
    [StringLength(10000, MinimumLength = 10,
        ErrorMessage = $"{nameof(Content)} must contain between 10 and 10000 characters")]
    public string? Content { get; set; }

    [ArrayStringLength(1, 10)]
    [Required]
    public ICollection<string> Tags { get; set; } = new List<string>();

    public bool IsPublished { get; set; }

    public ArticleEditModel()
    {
    }

    public ArticleEditModel(Post post)
    {
        Id = post.Id;
        Slug = post.Slug;
        Title = post.Title;
        IsPublished = post.IsPublished;
        Description = post.Description;
        Content = post.Content;
        Tags = post.Tags.Select(t => t.Name).ToList();
    }

    public Post ToPost()
    {
        return new Post
        {
            Id = this.Id,
            Slug = this.Slug!,
            Title = this.Title!,
            IsPublished = this.IsPublished,
            Description = this.Description!,
            Content = this.Content!,
            Tags = this.Tags.Select(t => new Tag { Name = t }).ToArray()
        };
    }
}