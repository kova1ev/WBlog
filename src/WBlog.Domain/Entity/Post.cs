namespace WBlog.Domain.Entity
{
    public class Post : BaseEntity
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public string Title { get; set; } = string.Empty;
        public string? Descriprion { get; set; }
        public string? Contetnt { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<Tag>? Tags { get; set; }// = new List<Tag>();
    }
}
