namespace WBlog.Admin.Models
{
    public class PageParameters
    {
        public int CurrentPage { get; set; } = 1;
        public int TotalItems { get; set; }
        public int ItemPerPage { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemPerPage);
    }
}