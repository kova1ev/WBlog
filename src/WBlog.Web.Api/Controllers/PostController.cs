using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WBlog.Shared.Models;
using WBlog.Application.Core.Interfaces;
using WBlog.Application.Core.Domain;
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
        public async Task<ActionResult<FiltredPostsModel>> Get([FromQuery] RequestOptions options)
        {
            var posts = await postService.GetPosts(options);

            return Ok(_mapper.Map<FiltredPosts>(posts));
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PostDetailsModel>> GetById(Guid id)
        {
            var entity = await postService.GetPostById(id);
            if (entity == null)
                return NotFound();
            return Ok(_mapper.Map<PostDetailsModel>(entity));

        }

        [AllowAnonymous]
        [HttpGet("{slug}")]
        public async Task<ActionResult<PostDetailsModel>> GetBySlug(string slug)
        {
            var entity = await postService.GetPostBySlug(slug);
            if (entity == null)
                return NotFound();
            return Ok(_mapper.Map<PostDetailsModel>(entity));
        }

        [HttpGet("{id}/tags")]
        public async Task<ActionResult<IEnumerable<TagModel>>> GetPostTags(Guid id)
        {
            var tags = await postService.GetPostTags(id);
            return Ok(_mapper.Map<IEnumerable<TagModel>>(tags));
        }

        #region Тестовая реализация проверить/пробебажить
        //testing
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return Ok(await postService.Delete(id));
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddPost([FromBody] PostEditModel value)
        {
            //todo продумать сохранение картинок
            //валидация
            if (!value.Tags.Any())
                return BadRequest(new ProblemDetails { Detail = "Tags is emppty" });
            PostEdit model = new PostEdit
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
        public async Task<ActionResult<bool>> UpdatePost([FromBody] PostEditModel value)
        {
            //todo продумать сохранение картинок
            //валидация
            if (value == null)
                return BadRequest();
            PostEdit model = new PostEdit
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

