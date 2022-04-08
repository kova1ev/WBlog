using System.ComponentModel.DataAnnotations;

namespace WBlog.Domain.Entity
{
    public abstract class BaseEntity
    {
        [Required]
        public Guid Id { get; set; }
    }
}
