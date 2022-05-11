using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WBlog.Application.Core.Dto;
using WBlog.Application.Core.Interfaces;
using WBlog.Application.Core.Services;
using AutoMapper;

namespace WBlog.Api.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService tagService;
        private readonly IMapper _mapper;
        public TagController(ITagService Service, IMapper mapper)
        {
            this.tagService = Service;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDto>>> Get()
        {
            var tags = await tagService.GetAllTags();
            return Ok(_mapper.Map<IEnumerable<TagDto>>(tags));
        }

        [AllowAnonymous]
        [HttpGet("popular")]
        public async Task<ActionResult<IEnumerable<PopularTagDto>>> GetPupular()
        {
            var tags = await tagService.GetTagsByPopularity();
            return Ok(_mapper.Map<IEnumerable<PopularTagDto>>(tags));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TagDto>> Get(Guid id)
        {
            var tag = await tagService.GetById(id);
            if (tag == null)
                return NotFound();
            return Ok(_mapper.Map<TagDto>(tag));
        }

        #region Тестовая реализация проверить/пробебажить
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] TagDto value)
        {
            if (value == null)
                return BadRequest();
            return Ok(await tagService.Save(value));
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] TagDto value)
        {
            if (value == null)
                return BadRequest();
            return Ok(await tagService.Update(value));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return Ok(await tagService.Delete(id));
        }
        #endregion
    }
}
