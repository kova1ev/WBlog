using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WBlog.Shared.Models;
using WBlog.Application.Core.Interfaces;
using AutoMapper;
using WBlog.Application.Core.Domain.Entity;

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
        public async Task<ActionResult<IEnumerable<TagModel>>> Get()
        {
            var tags = await tagService.GetAllTags();
            return Ok(_mapper.Map<IEnumerable<TagModel>>(tags));
        }

        [AllowAnonymous]
        [HttpGet("popular")]
        public async Task<ActionResult<IEnumerable<PopularTagModel>>> GetPupular()
        {
            var tags = await tagService.GetTagsByPopularity();
            return Ok(_mapper.Map<IEnumerable<PopularTagModel>>(tags));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TagModel>> Get(Guid id)
        {
            var tag = await tagService.GetById(id);
            if (tag == null)
                return NotFound();
            return Ok(_mapper.Map<TagModel>(tag));
        }

        #region Тестовая реализация проверить/пробебажить
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] Tag value)
        {
            if (value == null)
                return BadRequest();
            return Ok(await tagService.Save(value));
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] Tag value)
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
