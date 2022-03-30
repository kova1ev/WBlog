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
        readonly IPostRepository postRepository;

        public PostController(IPostRepository repo)
        {
            postRepository = repo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostIndexDto>>> Get([FromQuery] RequestOptions options)
        {
            var posts = await postRepository.GetPosts(options);
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
            var entity = await postRepository.GetPostById(id);
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
                Tags = entity.Tags?.Select(t => t?.Name).ToArray()
            });

        }

        [HttpGet("slug")]
        public async Task<ActionResult<PostDetailsDto>> GetBySlug(string slug)
        {
            var entity = await postRepository.GetPostBySlug(slug);
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
                Tags = entity.Tags?.Select(t => t?.Name).ToArray()
            });
        }

        [HttpGet("{id}/tags")]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetPostTags(Guid id)
        {
            var tags = await postRepository.GetPostsTags(id);
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
            return Ok(await postRepository.Remove(id));
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddPost([FromBody] Post value)
        {
            // todo продумать сохранение тегов!!!
            //todo продумать сохранение картинок
            //валидация
            if (value == null)
                return BadRequest();
            return Ok(await postRepository.Add(value));
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdatePost([FromBody] Post value)
        {
            // todo продумать сохранение тегов!!!
            //todo продумать сохранение картинок
            //валидация
            if (value == null)
                return BadRequest();
            return Ok(await postRepository.Add(value));
        }
        #endregion
    }

}

