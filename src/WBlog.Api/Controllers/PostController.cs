﻿using Microsoft.AspNetCore.Mvc;
using WBlog.Core;
using WBlog.Core.Dto;
using WBlog.Core.Repository.Interface;
using WBlog.Domain.Entity;

namespace WBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        readonly IPostRepository _postRepository;

        public PostController(IPostRepository repo)
        {
            _postRepository = repo;
        }

        //todo make url "{tag}/{serch}/{page}-{int} => читаемый вид наврено на клинете 
        //todo  /tag= , serch= , count= , page= ,itemPerPage=
        //itemPerPage - max  100 or below  Task<ActionResult<IEnumerable<PostIndexDto>>>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> Get(int offset, int limit, SortState state)
        {
            if (limit == 0) limit = 10;
            if (limit > 100) limit = 100;
            var posts = await _postRepository.GetPosts(offset, limit, state);
            //int count = posts.Count();
            return Ok(posts);
        }


        [HttpGet("id")]
        public async Task<ActionResult<PostDetailsDto>> GetById(Guid id)
        {
            var entity = await _postRepository.GetPostById(id);
            if (entity == null) return NotFound();
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
                Tags = entity.Tags?.Select(t => t?.Name).ToArray()
            });

        }

        [HttpGet("slug")]
        public async Task<ActionResult<PostDetailsDto>> GetBySlug(string slug)
        {
            var entity = await _postRepository.GetPostBySlug(slug);
            if (entity == null) return NotFound();
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
                Tags = entity.Tags?.Select(t => t?.Name).ToArray()
            });
        }


        [HttpGet("tag")]
        public async Task<ActionResult<IEnumerable<PostIndexDto>>> GetByTag(string tag, int offset, int limit, SortState state)
        {
            if (!string.IsNullOrWhiteSpace(tag))
            {
                if (limit == 0) limit = 10;
                if (limit > 100) limit = 100;
                IEnumerable<Post> postList = await _postRepository.GetPostsByTag(tag, offset, limit, state);
                if (postList.Count() > 0)
                {
                    return Ok(postList.Select(t =>
                                new PostIndexDto
                                {
                                    Id = t.Id,
                                    Title = t.Title,
                                    DateCreated = t.DateCreated,
                                    DateUpdated = t.DateUpdated,
                                    Descriprion = t.Descriprion,
                                    ImagePath = t.ImagePath,
                                }).ToArray());
                }
            }
            return NotFound();
        }

        [HttpGet("query")]
        public async Task<ActionResult<IEnumerable<PostIndexDto>>> GetByQuery(string serchstr, int offset, int limit, SortState state)
        {
            if (!string.IsNullOrWhiteSpace(serchstr))
            {
                if (limit == 0) limit = 10;
                if (limit > 100) limit = 100;
                IEnumerable<Post> postList = await _postRepository.SearchPost(serchstr, offset, limit, state);
                if (postList.Count() > 0)
                {
                    return Ok(postList.Select(t =>
                                new PostIndexDto
                                {
                                    Id = t.Id,
                                    Title = t.Title,
                                    DateCreated = t.DateCreated,
                                    DateUpdated = t.DateUpdated,
                                    Descriprion = t.Descriprion,
                                    ImagePath = t.ImagePath,
                                }).ToArray());
                }
            }
            return NotFound();
        }



    }

}

