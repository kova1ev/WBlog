namespace WBlog.Shared.Models
{
    public class PageParametrs
    {
        public int CurrentPage { get; set; } = 1;
        public int TotalItems { get; set; }
        public int ItemPerPage { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemPerPage);
    }
}