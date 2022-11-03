﻿using WBlog.Core.Interfaces;
using WBlog.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using WBlog.Core.Exceptions;
using WBlog.Core.Domain;

namespace WBlog.Core.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        this._tagRepository = tagRepository;
    }

    public async Task<FiltredData<Tag>> GetTags(TagRequestOptions options)
    {
        FiltredData<Tag> result = new FiltredData<Tag>();

        IQueryable<Tag> tags = _tagRepository.Tags.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(options.Query))
        {
            tags = tags.Where(t => t.Name.Contains(options.Query.Trim()));
        }

        result.TotalItems = await tags.CountAsync();
        result.Data = await tags.Skip(options.OffSet).Take(options.Limit).ToListAsync();
        return result;
    }

    public async Task<IEnumerable<Tag>> GetTagsByPopularity(int count)
    {
        var result = await _tagRepository.Tags.AsNoTracking()
           .Include(t => t.Posts.Where(p => p.IsPublished == true))
            .Where(t => t.Posts.Where(p => p.IsPublished == true).Count() > 0)
            .Take(count)
            .OrderByDescending(t => t.Posts.Where(p => p.IsPublished == true).Count())
            .ToListAsync();

        return result;
    }

    public async Task<Tag> GetById(Guid id)
    {
        var tag = await _tagRepository.GetById(id);
        if (tag == null)
            throw new ObjectNotFoundExeption($"Tag with id \'{id}\' not found.");
        return tag;
    }

    public async Task<Tag?> GetByName(string name)
    {
        var tag = await _tagRepository.GetByName(name);
        return tag;
    }

    public async Task<bool> Save(Tag entity)
    {
        string validName = entity.Name.Trim();
        var existingTag = await GetByName(validName);
        if (existingTag != null)
            throw new ObjectExistingException($"Tag with this name \'{existingTag.Name}\' is existing.");
        return await _tagRepository.Add(new Tag { Name = validName });
    }

    public async Task<bool> Update(Tag entity)
    {
        var existingTag = await GetById(entity.Id);

        string validName = entity.Name.Trim();
        var existingtagByName = await GetByName(validName);
        if (existingtagByName != null && existingtagByName.Id != entity.Id)
            throw new ObjectExistingException($"Tag with this name \'{validName}\' is existing.");

        existingTag!.Name = validName;
        return await _tagRepository.Update(existingTag);
    }

    public async Task<bool> Delete(Guid id)
    {
        Tag tag = await GetById(id);
        return await _tagRepository.Delete(tag);
    }
}