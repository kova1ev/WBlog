namespace WBlog.WebUI.Services;

public interface IBlogClient
{
    public Task<T> GetAsync<T>(string urlString) where T : class;
}

