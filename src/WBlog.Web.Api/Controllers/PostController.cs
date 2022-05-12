using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WBlog.Shared.Dto;
using WBlog.Application.Core.Interfaces;
using WBlog.Application.Core.Models;
using WBlog.Application.Core;
using AutoMapper;

namespace WBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class PostController : ControllerBase
    {
        readonly IPostService postService;
        readonly IMapper _mapper;

        public PostController(IPostService service, IMapper mapper)
        {
            postService = service;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<FiltredPostsDto>> Get([FromQuery] RequestOptions options)
        {
            var posts = await postService.GetPosts(options);

            return Ok(_mapper.Map<FiltredPosts>(posts));
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PostDetailsDto>> GetById(Guid id)
        {
            var entity = await postService.GetPostById(id);
            if (entity == null)
                return NotFound();
            return Ok(_mapper.Map<PostDetailsDto>(entity));

        }

        [AllowAnonymous]
        [HttpGet("{slug}")]
        public async Task<ActionResult<PostDetailsDto>> GetBySlug(string slug)
        {
            var entity = await postService.GetPostBySlug(slug);
            if (entity == null)
                return NotFound();
            return Ok(_mapper.Map<PostDetailsDto>(entity));
        }

        [HttpGet("{id}/tags")]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetPostTags(Guid id)
        {
            var tags = await postService.GetPostTags(id);
            return Ok(_mapper.Map<IEnumerable<TagDto>>(tags));
        }

        #region Тестовая реализация проверить/пробебажить
        //testing
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return Ok(await postService.Delete(id));
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddPost([FromBody] PostEditDto value)
        {
            //todo продумать сохранение картинок
            //валидация
            if (!value.Tags.Any())
                return BadRequest(new ProblemDetails { Detail = "Tags is emppty" });
            PostModel model = new PostModel
            {
                Title = value.Title,
                Slug = value.Slug,
                Description = value.Description,
                Content = value.Content,
                Tags = value.Tags
            };
            return Ok(await postService.Save(model));
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdatePost([FromBody] PostEditDto value)
        {
            //todo продумать сохранение картинок
            //валидация
            if (value == null)
                return BadRequest();
            PostModel model = new PostModel
            {
                Title = value.Title,
                Slug = value.Slug,
                Description = value.Description,
                Content = value.Content,
                Tags = value.Tags
            };
            return Ok(await postService.Update(model));
        }

        [HttpPut("{id:guid}/publish")]
        public async Task<ActionResult<bool>> Publish(Guid id, [FromQuery] bool publish)
        {
            return Ok(await postService.PublishPost(id, publish));
        }

        #endregion
    }

}

