namespace WBlog.Application.Core.Dto
{
    public class PagedPosts : BaseRequestDto
    {
        public int TotalItems { get; set; }
        public IEnumerable<PostIndexDto> Data { get; set; } = Enumerable.Empty<PostIndexDto>();
    }
}
