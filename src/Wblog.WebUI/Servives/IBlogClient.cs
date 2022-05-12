
namespace Wblog.WebUI.Servises
{
    public interface IBlogClient
    {
        public Task<T> GetAsync<T>(string urlParameters) where T : class;
    }
}
