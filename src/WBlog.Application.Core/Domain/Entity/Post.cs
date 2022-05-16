using System.ComponentModel.DataAnnotations;

namespace WBlog.Application.Core.Domain.Entity
{
    public class Post : BaseEntity
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;

        [Required]
        public string Title { get; set; } = string.Empty;
        [Required] // TODO: index is unique
        public string Slug { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        //public Image Image { get; set; }
        public bool IsPublished { get; set; }

        [Required]
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
