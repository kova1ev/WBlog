namespace Wblog.WebUI.Servises
{
    public interface IBlogClient
    {
        public Task<T> GetAsync<T>(string urlString) where T : class;

        public Task<bool> DeleteAsync(string urlString);
    }
}
