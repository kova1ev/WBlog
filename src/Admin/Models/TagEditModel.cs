using System.ComponentModel.DataAnnotations;
using WBlog.Core.Domain.Entity;

namespace WBlog.Admin.Models;

public class TagEditModel
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1, ErrorMessage = $"{nameof(Name)} must contain between 1 and 50 characters")]
    public string? Name { get; set; }

    public TagEditModel()
    {
    }

    public TagEditModel(Tag tag)
    {
        Id = tag.Id;
        Name = tag.Name;
    }

    public Tag ToTag()
    {
        return new Tag { Id = this.Id, Name = this.Name! };
    }
}