namespace WBlog.WebUI.Models;

public class FilteredDataModel<T>
{
    public int TotalItems { get; set; }
    public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
}
