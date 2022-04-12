using WBlog.Application.Core.Dto;

namespace WBlog.Application.Core.Services
{
    public interface ITagService
    {
        Task<IEnumerable<TagDto>> GetAllTags();
        Task<IEnumerable<PopularTagDto>> GetTagsByPopularity();
        Task<TagDto?> GetByName(string name);
        Task<TagDto?> GetById(Guid id);
        Task<bool> Save(TagDto entity);
        Task<bool> Delete(Guid id);
        Task<bool> Update(TagDto entity);
    }
}
