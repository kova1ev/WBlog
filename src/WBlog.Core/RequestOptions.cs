namespace WBlog.Core
{
    public class RequestOptions : PageOptions
    {
        public SortState State { get; set; } = SortState.DateDesc;
        public string? Query { get; set; }
        public string? Tag { get; set; }
        public bool Publish { get; set; }

    }
}