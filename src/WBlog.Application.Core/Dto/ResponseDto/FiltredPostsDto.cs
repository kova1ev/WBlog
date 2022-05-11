namespace WBlog.Application.Core.Dto
{
    public class FiltredPostsDto : BaseResponseDto
    {
        public int TotalItems { get; set; }
        public IEnumerable<PostIndexDto> Data { get; set; } = Enumerable.Empty<PostIndexDto>();
    }
}