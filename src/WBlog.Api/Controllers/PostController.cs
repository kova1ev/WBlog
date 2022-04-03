﻿using Microsoft.AspNetCore.Mvc;
using WBlog.Core;
using WBlog.Core.Dto;
using WBlog.Domain.Repository.Interface;
using WBlog.Core.Services;
using WBlog.Domain.Entity;

namespace WBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        readonly IPostService postService;

        public PostController( IPostService service)
        {
            postService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostIndexDto>>> Get([FromQuery] RequestOptions options)
        {
            var posts = await postService.GetPosts(options);
            return Ok(posts.Select(p => new PostIndexDto
            {
                Id = p.Id,
                Title = p.Title,
                DateCreated = p.DateCreated,
                DateUpdated = p.DateUpdated,
                Descriprion = p.Descriprion,
                ImagePath = p.ImagePath
            }).ToArray());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PostDetailsDto>> GetById(Guid id)
        {
            var entity = await postService.GetPostById(id);
            if (entity == null)
                return NotFound();
            return Ok(new PostDetailsDto
            {
                Id = entity.Id,
                DateUpdated = entity.DateUpdated,
                DateCreated = entity.DateCreated,
                Title = entity.Title,
                Slug = entity.Slug,
                Descriprion = entity.Descriprion,
                Contetnt = entity.Contetnt,
                ImagePath = string.Empty,
                IsPublished = entity.IsPublished,
                Tags = entity.Tags.Select(t => t.Name).ToList()
            });

        }

        [HttpGet("slug")]
        public async Task<ActionResult<PostDetailsDto>> GetBySlug(string slug)
        {
            var entity = await postService.GetPostBySlug(slug);
            if (entity == null)
                return NotFound();
            return Ok(new PostDetailsDto
            {
                Id = entity.Id,
                DateUpdated = entity.DateUpdated,
                DateCreated = entity.DateCreated,
                Title = entity.Title,
                Slug = entity.Slug,
                Descriprion = entity.Descriprion,
                Contetnt = entity.Contetnt,
                ImagePath = string.Empty,
                IsPublished = entity.IsPublished,
                Tags = entity.Tags.Select(t => t.Name).ToArray()
            });
        }

        [HttpGet("{id}/tags")]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetPostTags(Guid id)
        {
            var tags = await postService.GetPostsTags(id);
            return Ok(tags.Select(t => new TagDto
            {
                Id = t.Id,
                Name = t.Name
            }));
        }
        #region Тестовая реализация проверить/пробебажить
        //testing
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return Ok(await postService.Delete(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AddPost([FromBody] PostEditDto post)
        {
            //todo продумать сохранение картинок
            //валидация
          //  if (post == null)
          //      return BadRequest();
           if (!post.Tags.Any())
                return BadRequest(new ProblemDetails { Detail="Tags is emppty"});
            return Ok(await postService.SavePost(post));
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdatePost([FromBody] PostEditDto value)
        {
            // todo продумать сохранение тегов!!!
            //todo продумать сохранение картинок
            //валидация
            if (value == null)
                return BadRequest();
            return Ok(await postService.Update(value));
        }

        [HttpPut("{id:guid}/publish")]
        public async Task<ActionResult<bool>> Publish(Guid id, [FromQuery] bool publish)
        {
            return Ok(await postService.PublishPost(id, publish));
        }

        #endregion
    }

}

