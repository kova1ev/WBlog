using WBlog.Domain.Entity;

namespace WBlog.Core.Repository.Interface
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<IEnumerable<Post>> GetPostsByTagAsync(string category);
        Task<IEnumerable<Post>> GetPostsByNameAsync(string name);
        Task<Post?> GetPostByIdAsync(int id);
        //Task<Post> GetPostBySlug(string slug);  ?
        Task<bool> SavePostAsync(Post post);
        Task<bool> UpdatePostAsync(Post post);
        Task<bool> DeletePostAsync(int id);
    }
}
