using System.ComponentModel.DataAnnotations;
using System.Windows.Markup;
using System.Xml.Linq;

namespace WBlog.Core.Domain.Entity;

public class Tag : BaseEntity
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string NormalizeName { get; set; } = string.Empty;

    public ICollection<Post> Posts { get; set; } = new List<Post>();
}