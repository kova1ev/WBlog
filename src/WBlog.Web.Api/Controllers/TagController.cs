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
        public async Task<ActionResult<IEnumerable<TagModel>>> Get()
        {
            var tags = await _tagService.GetAllTags();
            return Ok(_mapper.Map<IEnumerable<TagModel>>(tags));
        }

        [AllowAnonymous]
        [HttpGet("popular")]
        public async Task<ActionResult<IEnumerable<PopularTagModel>>> GetPupular()
        {
            var tags = await _tagService.GetTagsByPopularity();
            return Ok(_mapper.Map<IEnumerable<PopularTagModel>>(tags));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TagModel>> Get(Guid id)
        {
            var tag = await _tagService.GetById(id);
            if (tag == null)
                return NotFound();
            return Ok(_mapper.Map<TagModel>(tag));
        }

        #region
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] TagModel value)
        {
            try
            {
                var tag = _mapper.Map<Tag>(value);
                return Ok(await _tagService.Save(tag));
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails { Detail = ex.Message });
            }

        }

        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] TagModel value)
        {
            try
            {
                var tag = _mapper.Map<Tag>(value);
                return Ok(await _tagService.Update(tag));
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails { Detail = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            try
            {
                return Ok(await _tagService.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails { Detail = ex.Message });
            }
        }
        #endregion
    }
}
