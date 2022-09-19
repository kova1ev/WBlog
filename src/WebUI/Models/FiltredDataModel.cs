namespace WBlog.WebUI.Models;

public class FiltredDataModel<T>
{
    public int TotalItems { get; set; }
    public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
}
