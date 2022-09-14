namespace WBlog.Shared.Models
{
    public class FiltredDataModel<T> : BaseModel
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
    }
}