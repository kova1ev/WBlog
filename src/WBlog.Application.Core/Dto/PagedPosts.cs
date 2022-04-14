namespace WBlog.Application.Core.Dto
{
    public class PagedPosts : BaseRequestDto
    {
        public int TotalItems { get; set; }
        public PostIndexDto[]? Data { get; set; } 
    }
}
