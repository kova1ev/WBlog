namespace Wblog.WebUI.Models
{
    public class PageParametrs
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int ItemPerPage { get; set; } = 10;
        public int TotalPages
        {
            get => (int)Math.Ceiling((decimal)TotalItems / ItemPerPage);
        }
    }
}