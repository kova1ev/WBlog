using WBlog.Domain.Entity;

namespace WBlog.Core.Repository.Interface
{
    public interface IPostRepository
    {
        Task<IQueryable<Post>> GetAllPostsAsync();
        Task<IQueryable<Post>> GetPostByTagAsync(string category);
        Task<IQueryable<Post>> GetPostsByNameAsync(string name);
        Task<Post> GetPostByIdAsync(int id);
        //Task<Post> GetPostBySlug(string slug);  ?
        Task<bool> SavePostAsync(Post post);
        Task<bool> UpdatePostAsync(Post post);
        Task<bool> DeletePostAsync(int id);
    }
}
