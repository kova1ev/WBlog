using WBlog.Application.Core.Domain.Entity;

namespace WBlog.Application.Core.Domain
{
    public class FiltredData<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
    }
}
