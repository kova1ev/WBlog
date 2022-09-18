using WBlog.Core.Domain.Entity;

namespace WBlog.Admin.Models;

public class TagViewModel
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public TagViewModel()
    {
    }

    public TagViewModel(Tag tag)
    {
        Id = tag.Id;
        Name = tag.Name;
    }
}