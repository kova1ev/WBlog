using WBlog.Domain.Entity;

namespace WBlog.Core.Dto.ResponseDto
{
    public class PagedPosts
    {
        public int TotalItems { get; set; }
        public PostIndexDto[] Data { get; set; }
    }
}
