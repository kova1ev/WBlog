namespace WBlog.Shared.Models
{
    public class FiltredPostsModel : BaseModel
    {
        public int TotalItems { get; set; }
        public IEnumerable<PostIndexModel> Data { get; set; } = Enumerable.Empty<PostIndexModel>();
    }
}