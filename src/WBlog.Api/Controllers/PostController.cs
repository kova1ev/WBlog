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


        [HttpGet("id={id}")]
        public async Task<ActionResult<PostDetailsDto>> GetById(Guid id)
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

        [HttpGet("tag={tag}")]
        public async Task<ActionResult<IEnumerable<PostIndexDto>>> GetByTag(string? tag)
        {
            if (!string.IsNullOrWhiteSpace(tag))
            {
                IEnumerable<Post> postList = await _postRepository.GetPostsByTagAsync(tag);
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
                            ImagePath = t.ImagePath
                        }).ToArray());
                }
            }
            return NotFound();
        }

        [HttpGet("name={name}")]
        public async Task<ActionResult<IEnumerable<PostIndexDto>>> GetByName(string? name)
        {
            IEnumerable<Post> postList = Enumerable.Empty<Post>();
            if (!string.IsNullOrWhiteSpace(name))
            {
                postList = await _postRepository.GetPostsByNameAsync(name);
            }
            return Ok(postList.Select(t =>
                 new PostIndexDto
                 {
                     Id = t.Id,
                     Title = t.Title,
                     DateCreated = t.DateCreated,
                     DateUpdated = t.DateUpdated,
                     Descriprion = t.Descriprion,
                     ImagePath = t.ImagePath
                 }).ToArray());
        }

    }

}

