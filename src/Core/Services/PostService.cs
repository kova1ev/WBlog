using WBlog.Core.Interfaces;
using WBlog.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using WBlog.Core.Domain;
using WBlog.Core.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace WBlog.Core.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly ITagService _tagService;

    public PostService(IPostRepository postRepository, ITagService tagService)
    {
        this._postRepository = postRepository;
        this._tagService = tagService;
    }


    public async Task<Post> GetPostByIdAsync(Guid id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
            throw new ObjectNotFoundException($"Article with id \'{id}\' not found ");
        return post;
    }

    public async Task<Post?> GetPostBySlugAsync(string? slug)
    {
        var normalizeSlug = NormalizeSlug(slug);
        var post = await _postRepository.GetBySlugAsync(normalizeSlug);
        return post;
    }

    //TODO если не выбран тег, то поиск сделать и в тегах и в заголовках
    public async Task<FiltredData<Post>> GetPostsAsync(ArticleRequestOptions options)
    {
        FiltredData<Post> responseData = new FiltredData<Post>();
        IQueryable<Post> posts = _postRepository.Posts.AsNoTracking();

        if (options.Publish)
            posts = posts.Where(p => p.IsPublished);

        if (!string.IsNullOrWhiteSpace(options.Tag))
        {
            var normalizename = _tagService.NormalizeName(options.Tag.Trim());
            posts = from p in posts
                    from t in p.Tags
                    where t.NormalizeName == normalizename
                    select p;
        }

        if (!string.IsNullOrWhiteSpace(options.Query))
        {
            posts = posts.Where(p =>  EF.Functions.Like(p.Title,$"%{options.Query.Trim()}%"));
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

        responseData.TotalItems = await posts.CountAsync();
        responseData.Data = await posts.Skip(options.OffSet).Take(options.Limit).ToListAsync();
        return responseData;
    }

    public async Task<IEnumerable<Tag>> GetPostTagsAsync(Guid id)
    {
        var tags = await _postRepository.Posts.AsNoTracking()
            .Where(p => p.Id == id)
            .SelectMany(p => p.Tags.Select(t => t))
            .ToListAsync();
        return tags;
    }

    #region

    public async Task<bool> PublishPostAsync(Guid id, bool publish)
    {
        var post = await GetPostByIdAsync(id);
        post.DateUpdated = DateTime.UtcNow;
        post.IsPublished = publish;
        return await _postRepository.UpdateAsync(post);
    }

    public async Task<bool> SaveAsync(Post entity)
    {
        //TODO save image
        string validSlug = entity.Slug.Trim().Replace(" ", "-");
        var post = await GetPostBySlugAsync(validSlug);
        if (post != null)
            throw new ObjectExistingException($"Artcile with this slug \'{validSlug}\' is existing");

        entity.Id = default;
        entity.Title = entity.Title.Trim();
        entity.Description = entity.Description.Trim();
        entity.Slug = validSlug;
        entity.NormalizeSlug = NormalizeSlug(entity.Slug);
        entity.Tags = (ICollection<Tag>)await UpdatePostTags(entity);

        return await _postRepository.AddAsync(entity);
    }

    public async Task<bool> UpdateAsync(Post entity)
    {
        var existingPost = await GetPostByIdAsync(entity.Id);

        string validSlug = entity.Slug.Trim().Replace(" ", "-");
        var postByFreeSlug = await GetPostBySlugAsync(validSlug);
        if (postByFreeSlug != null && postByFreeSlug.Id != existingPost.Id)
            throw new ObjectExistingException($"Artcile with this slug \'{validSlug}\' is existing");

        existingPost.Title = entity.Title.Trim();
        existingPost.Description = entity.Description.Trim();
        existingPost.Content = entity.Content;
        existingPost.Slug = validSlug;
        entity.NormalizeSlug = NormalizeSlug(entity.Slug);
        existingPost.IsPublished = entity.IsPublished;
        existingPost.DateUpdated = DateTime.UtcNow;
        existingPost.Tags = (ICollection<Tag>)await UpdatePostTags(entity);

        return await _postRepository.UpdateAsync(existingPost);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Post post = await GetPostByIdAsync(id);
        return await _postRepository.DeleteAsync(post);
    }

    #endregion

    ////////
    private async Task<IEnumerable<Tag>> UpdatePostTags(Post post)
    {
        var tagList = new List<Tag>();
        foreach (var item in post.Tags)
        {
            var tag = await _tagService.GetByNameAsync(item.Name);
            if (tag != null)
                tagList.Add(tag);
            else
            {
                await _tagService.SaveAsync(item);
                tagList.Add(item);
            }
        }

        return tagList;
    }

    /// <summary>
    /// Normalize Slug To Upper case
    /// </summary>
    /// <param name="slug">Post slug</param>
    /// <returns></returns>
    [return: NotNullIfNotNull("slug")]
    public string? NormalizeSlug(string? slug)
    {
        return slug == null ? null : slug.ToUpper();
    }
}