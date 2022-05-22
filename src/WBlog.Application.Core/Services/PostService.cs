using WBlog.Application.Core.Interfaces;
using WBlog.Application.Core.Domain.Entity;
using WBlog.Application.Core;
using Microsoft.EntityFrameworkCore;
using WBlog.Application.Core.Domain;
using WBlog.Application.Core.Exceptions;
using System.Text.RegularExpressions;

namespace WBlog.Application.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly ITagRepository tagRepository;

        public PostService(IPostRepository postRepository, ITagRepository tagRepository)
        {
            this.postRepository = postRepository;
            this.tagRepository = tagRepository;
        }


        public async Task<Post?> GetPostById(Guid id)
        {
            var post = await postRepository.GetById(id);
            if (post != null)
            {
                return post;
            }
            return null;
        }

        public async Task<Post?> GetPostBySlug(string slug)
        {
            var post = await postRepository.Posts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Slug == slug);
            if (post != null)
            {
                return post;
            }
            return null;
        }

        //todo если не выбран тег, то поиск сделать и в тегах и в заголовках
        public async Task<FiltredPosts> GetPosts(RequestOptions options)
        {
            FiltredPosts responseData = new();
            IQueryable<Post> posts = postRepository.Posts.AsNoTracking();
            if (options.Publish)
                posts = posts.Where(p => p.IsPublished);
            if (!string.IsNullOrWhiteSpace(options.Tag))
            {
                posts = from p in posts
                        from t in p.Tags
                        where t.Name.ToLower() == options.Tag.ToLower()
                        select p;
            }
            if (!string.IsNullOrWhiteSpace(options.Query))
            {
                posts = posts.Where(p => p.Title.ToLower().Contains(options.Query.Trim().ToLower()));
            }

            switch (options.State)
            {
                case SortState.DateAsc:
                    posts = posts.OrderBy(p => p.DateCreated);
                    break;
                default:
                    posts = posts.OrderByDescending(p => p.DateCreated);
                    break;
            }
            responseData.TotalItems = posts.Count();
            responseData.Data = await posts.Skip(options.OffSet).Take(options.Limit).ToListAsync();
            return responseData;
        }

        public async Task<IEnumerable<Tag>> GetPostTags(Guid id)
        {
            var tags = await postRepository.Posts.AsNoTracking()
                .Where(p => p.Id == id)
                .SelectMany(p => p.Tags.Select(t => t))
                .ToListAsync();
            return tags;
        }

        #region
        public async Task<bool> PublishPost(Guid id, bool publish)
        {
            Post? post = await postRepository.GetById(id);
            if (post == null)
                throw new ObjectNotFoundExeption($"Article with id \'{id}\' not found ");
            post.DateUpdated = DateTime.Now;
            post.IsPublished = publish;
            return await postRepository.Update(post);
        }

        public async Task<bool> Save(Post entity)
        {
            // save image
            string validSlug = entity.Slug.Trim().Replace(" ", "-");
            var post = await GetPostBySlug(validSlug);
            if (post != null)
                throw new ObjectExistingException($"Artcile with this slug \'{validSlug}\' is existing");

            entity.Id = default;
            entity.Title = entity.Title.Trim();
            entity.Description = entity.Description.Trim();
            entity.Content = entity.Content;
            entity.Slug = validSlug;
            entity.Tags = (ICollection<Tag>)await SaveTagsInPost(entity);

            return await postRepository.Add(entity);
        }

        public async Task<bool> Update(Post entity)
        {
            Post? post = await postRepository.GetById(entity.Id);
            if (post == null)
                throw new ObjectNotFoundExeption($"Article with id \'{entity.Id}\' not found ");

            string validSlug = entity.Slug.Trim().Replace(" ", "-");
            var post1 = await GetPostBySlug(validSlug);
            if (post1 != null && post1.Id != post.Id)
                throw new ObjectExistingException($"Artcile with this slug \'{validSlug}\' is existing");

            post.Title = entity.Title.Trim();
            post.Description = entity.Description.Trim();
            post.Content = entity.Content;
            post.Slug = validSlug;

            post.DateUpdated = DateTime.Now;
            post.Tags = (ICollection<Tag>)await SaveTagsInPost(entity);

            return await postRepository.Update(post);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await postRepository.Delete(id);
        }
        #endregion

        ////////
        private async Task<IEnumerable<Tag>> SaveTagsInPost(Post post)
        {
            var tagList = new List<Tag>();
            foreach (var item in post.Tags)
            {
                var tag = await tagRepository.GetByName(item.Name);
                if (tag != null)
                    tagList.Add(tag);
                else
                    tagList.Add(new Tag { Name = item.Name });
            }
            return tagList;
        }

    }
}
