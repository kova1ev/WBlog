namespace WBlog.Shared.Dto
{
    public class PopularTagDto : BaseResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PostCount { get; set; }

    }
}