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
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;
        public TagController(ITagService Service, IMapper mapper)
        {
            this._tagService = Service;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TagModel>>> Get()
        {
            var tags = await _tagService.GetAllTags();
            return Ok(_mapper.Map<IEnumerable<TagModel>>(tags));
        }

        [AllowAnonymous]
        [HttpGet("popular")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PopularTagModel>>> GetPupular()
        {
            var tags = await _tagService.GetTagsByPopularity();
            return Ok(_mapper.Map<IEnumerable<PopularTagModel>>(tags));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TagModel>> Get(Guid id)
        {
            var tag = await _tagService.GetById(id);
            return Ok(_mapper.Map<TagModel>(tag));
        }

        #region
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> Post([FromBody] TagModel entity)
        {
            var tag = _mapper.Map<Tag>(entity);
            return Ok(await _tagService.Save(tag));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> Put([FromBody] TagModel entity)
        {
            var tag = _mapper.Map<Tag>(entity);
            return Ok(await _tagService.Update(tag));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return Ok(await _tagService.Delete(id));
        }
        #endregion
    }
}
