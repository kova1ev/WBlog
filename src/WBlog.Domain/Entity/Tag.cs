namespace WBlog.Domain.Entity
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
