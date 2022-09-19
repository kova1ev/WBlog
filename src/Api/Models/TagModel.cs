using System.ComponentModel.DataAnnotations;

namespace WBlog.Api.Models;

public class TagModel : BaseModel
{
    public Guid Id { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 1, ErrorMessage = $"{nameof(Name)} must contain between 1 and 50 characters")]
    public string? Name { get; set; }
}
