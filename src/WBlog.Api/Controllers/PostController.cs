using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostIndexDto>>> Get()
        {
            IEnumerable<Post> postList = await _postRepository.GetAllPostsAsync();
            IEnumerable<PostIndexDto> dtos = postList.Select(t =>
                new PostIndexDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    DateCreated = t.DateCreated,
                    DateUpdated = t.DateUpdated,
                    Descriprion = t.Descriprion,
                    ImagePath = t.ImagePath
                }).ToArray();
            return Ok(dtos);
        }


        [HttpGet("id")]
        public async Task<ActionResult<PostDetailsDto>> Get(Guid id)
        {
            Post? post = await _postRepository.GetPostByIdAsync(id);
            if (post != null)
            {
                return Ok(new PostDetailsDto
                {
                    Id = post.Id,
                    DateUpdated = post.DateUpdated,
                    DateCreated = post.DateCreated,
                    Title = post.Title,
                    Descriprion = post.Descriprion,
                    Contetnt = post.Contetnt,
                    ImagePath = string.Empty,
                    Tags = post.Tags?.Select(t => t.Name).ToArray()
                }); ;
            }
            return NotFound();

        }

    }

}

