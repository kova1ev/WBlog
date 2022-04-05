using WBlog.Core.Dto.ResponseDto;
using WBlog.Core.Dto;
using WBlog.Domain.Entity;

namespace WBlog.Core.Services
{
    public interface IPostService
    {
        Task<PagedPosts> GetPosts(RequestOptions options);
        Task<PostDetailsDto?> GetPostById(Guid id);
        Task<IEnumerable<TagDto>> GetPostTags(Guid id);
        Task<PostDetailsDto?> GetPostBySlug(string slug);
        Task<bool> Save(PostEditDto postEdit);
        Task<bool> Update(PostEditDto post);
        Task<bool> Delete(Guid id);
        Task<bool> PublishPost(Guid id, bool publish);

    }
}
