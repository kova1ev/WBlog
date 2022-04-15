using WBlog.Application.Core.Interfaces;
using WBlog.Application.Domain.Entity;
using WBlog.Application.Core;
using WBlog.Application.Core.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace WBlog.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly ITagRepository tagRepository;
        private readonly IMapper mapper;

        public PostService(IPostRepository postRepository, ITagRepository tagRepository, IMapper mapper)
        {
            this.postRepository = postRepository;
            this.tagRepository = tagRepository;
            this.mapper = mapper;
        }


        public async Task<PostDetailsDto?> GetPostById(Guid id)
        {
            var post = await postRepository.GetById(id);
            if (post != null)
            {
                return mapper.Map<PostDetailsDto>(post);
            }
            return null;
        }

        public async Task<PostDetailsDto?> GetPostBySlug(string slug)
        {
            var post = await postRepository.Posts.FirstOrDefaultAsync(p => p.Slug.ToLower() == slug.ToLower());
            if (post != null)
            {
                return mapper.Map<PostDetailsDto>(post);
            }
            return null;
        }

        //todo  вынести tolower() в RequestOptions
        //todo если не выбран тег, то поиск сделать и в тегах и в заголовках
        public async Task<PagedPosts> GetPosts(RequestOptions options)
        {
            PagedPosts responseData = new();
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
            var result = await posts.Skip(options.OffSet).Take(options.Limit).ToListAsync();
            responseData.Data = mapper.Map<IEnumerable<PostIndexDto>>(result );

            return responseData;
        }

        public async Task<IEnumerable<TagDto>> GetPostTags(Guid id)
        {
            var  tags =  await postRepository.Posts.AsNoTracking()
                .Where(p => p.Id == id)
                .SelectMany(p => p.Tags.Select(t =>t))
                .ToListAsync();
            return mapper.Map<IEnumerable<TagDto>>(tags);
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
