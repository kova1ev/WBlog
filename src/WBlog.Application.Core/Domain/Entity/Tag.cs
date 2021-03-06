using System.ComponentModel.DataAnnotations;

namespace WBlog.Application.Core.Domain.Entity
{
    public class Tag : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
