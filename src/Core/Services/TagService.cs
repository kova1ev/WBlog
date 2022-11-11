using WBlog.Core.Interfaces;
using WBlog.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using WBlog.Core.Exceptions;
using WBlog.Core.Domain;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace WBlog.Core.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        this._tagRepository = tagRepository;
    }

    public async Task<FiltredData<Tag>> GetTagsAsync(TagRequestOptions options)
    {
        FiltredData<Tag> result = new FiltredData<Tag>();

        IQueryable<Tag> tags = _tagRepository.Tags.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(options.Query))
        {
            //todo serch in normalizename
            tags = tags.Where(t => t.Name.Contains(options.Query.Trim()));
        }

        result.TotalItems = await tags.CountAsync();
        result.Data = await tags.Skip(options.OffSet).Take(options.Limit).ToListAsync();
        return result;
    }

    public async Task<IEnumerable<Tag>> GetTagsByPopularityAsync(int count)
    {
        var result = await _tagRepository.Tags.AsNoTracking()
           .Include(t => t.Posts.Where(p => p.IsPublished == true))
            .Where(t => t.Posts.Where(p => p.IsPublished == true).Count() > 0)
            .Take(count)
            .OrderByDescending(t => t.Posts.Where(p => p.IsPublished == true).Count())
            .ToListAsync();

        return result;
    }

    public async Task<Tag> GetByIdAsync(Guid id)
    {
        var tag = await _tagRepository.GetByIdAsync(id);
        if (tag == null)
            throw new ObjectNotFoundExeption($"Tag with id \'{id}\' not found.");
        return tag;
    }

    public async Task<Tag?> GetByNameAsync(string name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        var normalizename = NormalizeName(name);
        var tag = await _tagRepository.GetByNameAsync(normalizename);
        return tag;
    }

    public async Task<bool> SaveAsync(Tag entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        // validate Name
        entity.Name = entity.Name.Trim();
        var existingTag = await GetByNameAsync(entity.Name);
        if (existingTag != null)
            throw new ObjectExistingException($"Tag with this name \'{existingTag.Name}\' is existing.");
        entity.NormalizeName = NormalizeName(entity.Name);
        entity.Id = Guid.NewGuid();
        return await _tagRepository.AddAsync(entity);
    }

    public async Task<bool> UpdateAsync(Tag entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        var existingTag = await GetByIdAsync(entity.Id);

        string validName = entity.Name.Trim();
        var existingtagByName = await GetByNameAsync(validName);
        if (existingtagByName != null && existingtagByName.Id != entity.Id)
            throw new ObjectExistingException($"Tag with this name \'{validName}\' is existing.");

        existingTag.Name = validName;
        existingTag.NormalizeName = NormalizeName(validName);
        return await _tagRepository.UpdateAsync(existingTag);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Tag tag = await GetByIdAsync(id);
        return await _tagRepository.DeleteAsync(tag);
    }


    /// <summary>
    /// Normalize the string to uppercase
    /// </summary>
    /// <param name="name">Tag name</param>
    /// <returns></returns>
    [return: NotNullIfNotNull("name")]
    public string? NormalizeName(string? name)
    {
        return name == null ? name : name.ToUpper();
    }
}