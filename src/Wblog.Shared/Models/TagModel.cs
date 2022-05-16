namespace WBlog.Shared.Models
{
    public class TagModel : BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
