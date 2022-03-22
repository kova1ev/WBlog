using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WBlog.Core.Data;
using WBlog.Core.Repository.Interface;
using WBlog.Domain.Entity;

namespace WBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        readonly IPostRepository _postRepository;
        readonly AppDbContext _dbContext;

        public PostController(IPostRepository repo, AppDbContext _dbContext)
        {
            _postRepository = repo;
            this._dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Post>> Get()
        {
            return await _postRepository.GetAllPostsAsync();
        }


        [HttpGet("id")]
        //public async Task<Post> Get(Guid id)
        //{
        //    return await _postRepository.GetPostByIdAsync(id);
        //}
        public object Get(Guid id)
        {
            return new
            {
                result = _dbContext.Posts.Select(p => p.Id==id new
                {
                    p.Id,
                    p.Title,
                    p.Contetnt,
                    p.Descriprion,
                    Tags = p.Tags.Select(t => new { t.Id, t.Name })
                    .ToList()
                })
             };
        }
    }

}
}
