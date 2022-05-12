namespace WBlog.Shared.Dto
{
    public class TagDto : BaseResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
