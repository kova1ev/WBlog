namespace WBlog.Application.Domain.Entity
{
    public class Image : BaseEntity
    {
        public string? Title { get; set; }
        public string? ImagePath { get; set; }
    }
}