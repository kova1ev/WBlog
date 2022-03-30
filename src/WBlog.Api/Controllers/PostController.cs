using Microsoft.AspNetCore.Mvc;
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

        //limit (itemPerPage) - max  100 or below => Task<ActionResult<IEnumerable<PostIndexDto>>>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> Get([FromQuery] RequestOptions options)
        {
            var posts = await _postRepository.GetPosts(options);
            //int count = posts.Count();
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
        public async Task<ActionResult<IEnumerable<PostIndexDto>>> GetByTag([FromQuery] RequestOptions options)
        {
            // if (!string.IsNullOrWhiteSpace(options.Tag))
            // {
                IEnumerable<Post> postList = await _postRepository.GetPostsByTag(options);
                if (postList.Count() > 0)
                {
                    return Ok(postList.Select(p =>
                                new PostIndexDto
                                {
                                    Id = p.Id,
                                    Title = p.Title,
                                    DateCreated = p.DateCreated,
                                    DateUpdated = p.DateUpdated,
                                    Descriprion = p.Descriprion,
                                    ImagePath = p.ImagePath
                                }).ToArray());
                }
            //}
            return NotFound();
        }

        [HttpGet("query")]
        public async Task<ActionResult<IEnumerable<PostIndexDto>>> GetByQuery([FromQuery] RequestOptions options)
        {
            // if (!string.IsNullOrWhiteSpace(options.Query))
            // {
                IEnumerable<Post> postList = await _postRepository.SearchPost(options);
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
            //}
            return NotFound();
        }

        [HttpGet("id/tags")]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetPostTags(Guid id)
        {
            var tags = await _postRepository.GetPostsTags(id);
            return Ok(tags.Select(t => new TagDto
            {
                Id = t.Id,
                Name = t.Name
            }));
        }

        //testing
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await _postRepository.RemovePost(id);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddPost([FromBody] Post post)
        {
            // todo продумать сохранение тегов!!!
            if (post == null)
                return BadRequest();
            return await _postRepository.AddPost(post);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdatePost([FromBody] Post post)
        {
            // todo продумать сохранение тегов!!!
            if (post == null)
                return BadRequest();
            return await _postRepository.AddPost(post);
        }

    }

}

