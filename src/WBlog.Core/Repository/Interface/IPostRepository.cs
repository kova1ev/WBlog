using WBlog.Domain.Entity;

namespace WBlog.Core.Repository.Interface
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<IEnumerable<Post>> GetPostsByTagAsync(string tag);
        Task<IEnumerable<Post>> GetPostsByNameAsync(string name);
        Task<Post?> GetPostByIdAsync(Guid id);
        //Task<Post> GetPostBySlug(string slug);  ?
        Task<bool> SavePostAsync(Post post);
        Task<bool> UpdatePostAsync(Post post);
        Task<bool> DeletePostAsync(int id);

        Post GetPostById(Guid id);
    }
}
