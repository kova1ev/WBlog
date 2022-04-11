using System.ComponentModel.DataAnnotations;

namespace WBlog.Application.Domain.Entity
{
    public abstract class BaseEntity
    {
        [Required]
        public Guid Id { get; set; }
    }
}
