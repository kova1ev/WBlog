using Microsoft.EntityFrameworkCore;
using WBlog.Domain.Data;
using WBlog.Domain.Repository.Interface;
using WBlog.Domain.Entity;

namespace WBlog.Domain.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext dbContext;
        public IQueryable<Tag> Tags => dbContext.Tags;

        public TagRepository(AppDbContext context)
        {
            dbContext = context;
        }

        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            return await Tags.AsNoTracking().ToListAsync();
        }

        public async Task<Tag?> GetById(Guid id)
        {
            return await Tags.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tag?> GetByName(string name)
        {
            return await Tags.FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
        }

        //todo подумать над реализацией
        public Task<IEnumerable<Tag>> GetPopulasTags()
        {
            throw new NotImplementedException();
        }
        #region Тестовая реализация проверить/пробебажить
        public async Task<bool> Add(Tag tag)
        {
            dbContext.Tags.Add(tag);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Remove(Guid id)
        {
            var entity = await GetById(id);
            if (entity == null)
                return false;
            dbContext.Tags.Remove(entity);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Tag tag)
       {
            //todo ловить exeptions
          dbContext.Entry(tag).State = EntityState.Modified;
           return await dbContext.SaveChangesAsync() > 0;
        }
        #endregion
    }
}
