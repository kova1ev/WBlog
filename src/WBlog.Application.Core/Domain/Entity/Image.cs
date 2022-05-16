namespace WBlog.Application.Core.Domain.Entity
{
    public class Image : BaseEntity
    {
        public string? Title { get; set; }
        public string? ImagePath { get; set; }
    }
}