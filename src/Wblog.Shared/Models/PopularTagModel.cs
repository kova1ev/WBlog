namespace WBlog.Shared.Models
{
    public class PopularTagModel : BaseModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int PostCount { get; set; }

    }
}