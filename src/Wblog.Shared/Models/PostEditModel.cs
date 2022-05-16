using System.ComponentModel.DataAnnotations;

namespace WBlog.Shared.Models
{
    public class PostEditModel : BaseModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Slug { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public IEnumerable<string> Tags { get; set; } = Enumerable.Empty<string>();
    }
}
