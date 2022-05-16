using WBlog.Application.Core.Domain.Entity;

namespace WBlog.Application.Core.Domain
{
    public class FiltredPosts
    {
        public int TotalItems { get; set; }
        public IEnumerable<Post> Data { get; set; } = Enumerable.Empty<Post>();
    }
}
