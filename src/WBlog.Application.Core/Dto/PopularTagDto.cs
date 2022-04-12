namespace WBlog.Application.Core.Dto
{
    public class PopularTagDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PostCount { get; set; }

    }
}