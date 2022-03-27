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

        //todo make url "{tag}/{serch}/{page}-{int} => читаемый вид наврено на клинете 
        //todo  /tag= , serch= , count= , page= ,itemPerPage=
        //itemPerPage - max  100 or below
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostIndexDto>>> Get()
        {
            return NotFound();
        }


        [HttpGet("id={id}")]
        public async Task<ActionResult<PostDetailsDto>> GetById(Guid id)
        {
            return NotFound();
        }

        [HttpGet("tag={tag}")]
        public async Task<ActionResult<IEnumerable<PostIndexDto>>> GetByTag(string? tag)
        {
            return NotFound();
            //if (!string.IsNullOrWhiteSpace(tag))
            //{
            //    IEnumerable<Post> postList = await _postRepository.GetPostsByTagAsync(tag);
            //    if (postList.Count() > 0)
            //    {
            //        return Ok(postList.Select(t =>
            //            new PostIndexDto
            //            {
            //                Id = t.Id,
            //                Title = t.Title,
            //                DateCreated = t.DateCreated,
            //                DateUpdated = t.DateUpdated,
            //                Descriprion = t.Descriprion,
            //                ImagePath = t.ImagePath
            //            }).ToArray());
            //    }
            //}
            //return NotFound();
        }

        [HttpGet("name={name}")]
        public async Task<ActionResult<IEnumerable<PostIndexDto>>> GetByName(string? name)
        {
            return NotFound();
            //IEnumerable<Post> postList = Enumerable.Empty<Post>();
            //if (!string.IsNullOrWhiteSpace(name))
            //{
            //    postList = await _postRepository.GetPostsByNameAsync(name);
            //}
            //return Ok(postList.Select(t =>
            //     new PostIndexDto
            //     {
            //         Id = t.Id,
            //         Title = t.Title,
            //         DateCreated = t.DateCreated,
            //         DateUpdated = t.DateUpdated,
            //         Descriprion = t.Descriprion,
            //         ImagePath = t.ImagePath
            //     }).ToArray());
        }

    }

}

