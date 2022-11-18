using WBlog.Core.Domain.Entity;

namespace WBlog.Admin.Models;

public class TagModel
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public TagModel()
    {
    }

    public TagModel(Tag tag)
    {
        Id = tag.Id;
        Name = tag.Name;
    }
}