using WBlog.Application.Domain.Entity;

namespace WBlog.Application.Core
{
    public class FiltredPosts
    {
        public int TotalItems { get; set; }
        public IEnumerable<Post> Data { get; set; } = Enumerable.Empty<Post>();
    }
}
