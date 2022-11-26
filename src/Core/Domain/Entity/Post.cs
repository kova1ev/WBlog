using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace WBlog.Core.Domain.Entity;

public class Post : BaseEntity
{
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime DateUpdated { get; set; } = DateTime.UtcNow;

    [Required] public string Title { get; set; } = string.Empty;

    [Required]
    public string Slug { get; set; } = string.Empty;
    [Required]
    public string? NormalizeSlug { get; set; }

    [Required] public string Description { get; set; } = string.Empty;

    [Required] public string Content { get; set; } = string.Empty;

    public bool IsPublished { get; set; }

    [Required] public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}