using WBlog.Shared.Models;

namespace Wblog.WebUI.Servises
{
    public interface IBlogClient
    {
        public Task<T> GetAsync<T>(string urlString) where T : class;
        public Task<bool> PutAsync(string urlString, TagModel entity);

        public Task<bool> DeleteAsync(string urlString);
    }
}
