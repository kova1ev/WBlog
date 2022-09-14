using WBlog.Core.Interfaces;
using WBlog.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using WBlog.Core.Domain;
using WBlog.Core.Exceptions;

namespace WBlog.Core.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly ITagRepository _tagRepository;

    public PostService(IPostRepository postRepository, ITagRepository tagRepository)
    {
        this._postRepository = postRepository;
        this._tagRepository = tagRepository;
    }


    public async Task<Post> GetPostById(Guid id)
    {
        var post = await _postRepository.GetById(id);
        if (post == null)
            throw new ObjectNotFoundExeption($"Article with id \'{id}\' not found ");
        return post;
    }

    public async Task<Post?> GetPostBySlug(string slug)
    {
        var post = await _postRepository.Posts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Slug == slug);
        return post;
    }

    //TODO если не выбран тег, то поиск сделать и в тегах и в заголовках
    public async Task<FiltredData<Post>> GetPosts(ArticleRequestOptions options)
    {
        FiltredData<Post> responseData = new FiltredData<Post>();
        IQueryable<Post> posts = _postRepository.Posts.AsNoTracking();

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
        var tags = await _postRepository.Posts.AsNoTracking()
            .Where(p => p.Id == id)
            .SelectMany(p => p.Tags.Select(t => t))
            .ToListAsync();
        return tags;
    }

    #region

    public async Task<bool> PublishPost(Guid id, bool publish)
    {
        var post = await GetPostById(id);
        post.DateUpdated = DateTime.Now;
        post.IsPublished = publish;
        return await _postRepository.Update(post);
    }

    public async Task<bool> Save(Post entity)
    {
        //TODO save image
        string validSlug = entity.Slug.Trim().Replace(" ", "-");
        var post = await GetPostBySlug(validSlug);
        if (post != null)
            throw new ObjectExistingException($"Artcile with this slug \'{validSlug}\' is existing");

        entity.Id = default;
        entity.Title = entity.Title.Trim();
        entity.Description = entity.Description.Trim();
        entity.Slug = validSlug;
        entity.Tags = (ICollection<Tag>)await SaveTagsInPost(entity);

        return await _postRepository.Add(entity);
    }

    public async Task<bool> Update(Post entity)
    {
        var existingPost = await GetPostById(entity.Id);

        string validSlug = entity.Slug.Trim().Replace(" ", "-");
        var postByFreeSlug = await GetPostBySlug(validSlug);
        if (postByFreeSlug != null && postByFreeSlug.Id != existingPost.Id)
            throw new ObjectExistingException($"Artcile with this slug \'{validSlug}\' is existing");

        existingPost.Title = entity.Title.Trim();
        existingPost.Description = entity.Description.Trim();
        existingPost.Content = entity.Content;
        existingPost.Slug = validSlug;
        existingPost.IsPublished = entity.IsPublished;
        existingPost.DateUpdated = DateTime.Now;
        existingPost.Tags = (ICollection<Tag>)await SaveTagsInPost(entity);

        return await _postRepository.Update(existingPost);
    }

    public async Task<bool> Delete(Guid id)
    {
        Post post = await GetPostById(id);
        return await _postRepository.Delete(post);
    }

    #endregion

    ////////
    private async Task<IEnumerable<Tag>> SaveTagsInPost(Post post)
    {
        var tagList = new List<Tag>();
        foreach (var item in post.Tags)
        {
            var tag = await _tagRepository.GetByName(item.Name);
            if (tag != null)
                tagList.Add(tag);
            else
                tagList.Add(new Tag { Name = item.Name.Trim() });
        }

        return tagList;
    }
}