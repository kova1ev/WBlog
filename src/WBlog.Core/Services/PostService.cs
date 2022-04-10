using Microsoft.EntityFrameworkCore;
using WBlog.Core.Dto;
using WBlog.Domain.Repository.Interface;
using WBlog.Domain.Entity;
using WBlog.Core.Dto.ResponseDto;

namespace WBlog.Core.Services
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


        public async Task<PostDetailsDto?> GetPostById(Guid id)
        {
            var post = await postRepository.GetById(id);
            if (post != null)
            {
                return new PostDetailsDto
                {
                    Id = post.Id,
                    DateUpdated = post.DateUpdated,
                    DateCreated = post.DateCreated,
                    Title = post.Title,
                    Slug = post.Slug,
                    Descriprion = post.Descriprion,
                    Contetnt = post.Contetnt,
                    IsPublished = post.IsPublished,
                    Tags = post.Tags.Select(t => t.Name).ToList()
                };
            }
            return null;
        }

        public async Task<PostDetailsDto?> GetPostBySlug(string slug)
        {
            var post = await postRepository.GetPostBySlug(slug);
            if (post != null)
            {
                return new PostDetailsDto
                {
                    Id = post.Id,
                    DateUpdated = post.DateUpdated,
                    DateCreated = post.DateCreated,
                    Title = post.Title,
                    Slug = post.Slug,
                    Descriprion = post.Descriprion,
                    Contetnt = post.Contetnt,
                    IsPublished = post.IsPublished,
                    Tags = post.Tags.Select(t => t.Name).ToList()
                };
            }
            return null;
        }

        //todo  вынести tolower() в RequestOptions
        //todo если не выбран тег, то поиск сделать и в тегах и в заголовках
        public async Task<PagedPosts> GetPosts(RequestOptions options)
        {
            PagedPosts responseData = new();
            IQueryable<Post> posts = postRepository.Posts;
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
                posts = posts.Where(p => p.Title.ToLower().Contains(options.Query.ToLower()));
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
            responseData.Data = await posts.Skip(options.OffSet).Take(options.Limit).Select(p => new PostIndexDto
            {
                Id = p.Id,
                DateUpdated = p.DateUpdated,
                DateCreated = p.DateCreated,
                Title = p.Title,
                Descriprion = p.Descriprion,
            }).ToArrayAsync();
            return responseData;
        }

        public async Task<IEnumerable<TagDto>> GetPostTags(Guid id)
        {
            return await postRepository.Posts
                .Where(p => p.Id == id)
                .SelectMany(p => p.Tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }))
                .ToListAsync();
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
            //todo create slug
            // save image
            Post post = new Post
            {
                Title = entity.Title!,
                Descriprion = entity.Descriprion!,
                Contetnt = entity.Contetnt!
            };
            foreach (string name in entity.Tags!)
            {
                var tag = await tagRepository.GetByName(name);
                if (tag != null)
                    post.Tags.Add(tag);
                else
                    post.Tags.Add(new Tag { Name = name });
            }
            return await postRepository.Add(post);
        }

        public async Task<bool> Update(PostEditDto entity)
        {
            //todo update slug
            Post? post = await postRepository.GetById(entity.Id);
            if (post == null)
                return false;
            post.Title = entity.Title!;
            post.Descriprion = entity.Descriprion!;
            post.Contetnt = entity.Contetnt!;
            post.Tags.Clear();
            foreach (string name in entity.Tags!)
            {
                var tag = await tagRepository.GetByName(name);
                if (tag != null)
                    post.Tags.Add(tag);
                else
                    post.Tags.Add(new Tag { Name = name });
            }
            return await postRepository.Update(post);
        }
        public async Task<bool> Delete(Guid id)
        {
            return await postRepository.Delete(id);
        }
        #endregion

    }
}
