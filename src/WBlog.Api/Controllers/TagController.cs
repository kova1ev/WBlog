using Microsoft.AspNetCore.Mvc;
using WBlog.Core.Dto;
using WBlog.Core.Repository.Interface;
using WBlog.Domain.Entity;

namespace WBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository tagRepository;
        public TagController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDto>>> Get()
        {
            var tags = await tagRepository.GetAllTags();
            if (tags == null || !tags.Any())
                return NotFound();
            return Ok(tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TagDto>> Get(Guid id)
        {
            var entity = await tagRepository.GetById(id);
            if (entity == null)
                return NotFound();
            return Ok(new TagDto { Id = entity.Id, Name = entity.Name });
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] Tag value)
        {
            if (value == null)
                return BadRequest();
            return Ok(await tagRepository.Add(value));
        }

        [HttpPut()]
        public async Task<ActionResult<bool>> Put([FromBody] Tag value)
        {
            if (value == null)
                return BadRequest();
            return Ok(await tagRepository.Update(value));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return Ok(await tagRepository.Remove(id));
        }
    }
}
