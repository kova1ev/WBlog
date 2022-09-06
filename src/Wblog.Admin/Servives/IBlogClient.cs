using WBlog.Shared.Models;

namespace Wblog.Admin.Servises
{
    public interface IBlogClient
    {
        public Task<T> GetAsync<T>(string urlString) where T : class;
        public Task<bool> PostAsync<T>(string urlString, T entity) where T : class;
        public Task<bool> PutAsync<T>(string urlString, T entity) where T : class;
        public Task<bool> DeleteAsync(string urlString);
        public Task<bool> PublishAsync(string urlString);
    }
}