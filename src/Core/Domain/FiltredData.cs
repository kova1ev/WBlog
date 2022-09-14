using WBlog.Core.Domain.Entity;

namespace WBlog.Core.Domain;

public class FiltredData<T>
{
    public int TotalItems { get; set; }
    public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
}