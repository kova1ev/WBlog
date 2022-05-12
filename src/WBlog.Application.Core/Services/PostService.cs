using WBlog.Application.Core.Interfaces;
using WBlog.Application.Core.Entity;
using WBlog.Application.Core;
using WBlog.Application.Core.Dto;
using Microsoft.EntityFrameworkCore;

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
            var post = await postRepository.Posts.Include(p=>p.Tags).FirstOrDefaultAsync(p => p.Slug == slug);
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

        #region Тестовая реализация проверить/пробебажить
        public async Task<bool> PublishPost(Guid id, bool publish)
        {
            Post? post = await postRepository.GetById(id);
            if (post == null)
                return false;
            post.DateUpdated = DateTime.Now;
            post.IsPublished = publish;
            return await postRepository.Update(post);
        }

        public async Task<bool> Save(PostEditDto entity)
        {
            // слуг не должен содержать  только англ буквы , пробелыменять на тире
            //TODO валидация полей
            //todo create slug
            // save image
            Post post = new Post
            {
                Title = entity.Title,
                Description = entity.Description,
                Content = entity.Content,
                Slug = entity.Slug
            };
            await SaveTagsInPost(post, entity.Tags);
            return await postRepository.Add(post);
        }

        public async Task<bool> Update(PostEditDto entity)
        {
            //TODO валидация полей
            //todo update slug
            Post? post = await postRepository.GetById(entity.Id);
            if (post == null)
                return false;
            post.Title = entity.Title;
            post.Slug = entity.Slug;
            post.Description = entity.Description;
            post.Content = entity.Content;
            post.Tags.Clear();
            await SaveTagsInPost(post, entity.Tags);
            return await postRepository.Update(post);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await postRepository.Delete(id);
        }
        #endregion


        private async Task SaveTagsInPost(Post post, IEnumerable<string> tags)
        {
            foreach (string name in tags)
            {
                var tag = await tagRepository.GetByName(name);
                if (tag != null)
                    post.Tags.Add(tag);
                else
                    post.Tags.Add(new Tag { Name = name });
            }
            await Task.CompletedTask;
        }

    }
}
