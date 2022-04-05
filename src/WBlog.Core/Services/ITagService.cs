using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBlog.Core.Dto;

namespace WBlog.Core.Services
{
    public interface ITagService
    {
        Task<IEnumerable<TagDto>> GetAllTags();
        Task<IEnumerable<TagDto>> GetPopulasTags();
        Task<TagDto?> GetByName(string name);
        Task<TagDto?> GetById(Guid id);
        Task<bool> Save(TagDto entity);
        Task<bool> Delete(Guid id);
        Task<bool> Update(TagDto entity);
    }
}
