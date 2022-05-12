using WBlog.Application.Core.Entity;
using WBlog.Application.Core.Models;

namespace WBlog.Application.Core.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllTags();
        Task<IEnumerable<Tag>> GetTagsByPopularity();
        Task<Tag?> GetByName(string name);
        Task<Tag?> GetById(Guid id);
        //todo save tag or tagmodel
        Task<bool> Save(Tag entity);
        //todo save tag or tagmodel
        Task<bool> Update(Tag entity);
        Task<bool> Delete(Guid id);
    }
}
