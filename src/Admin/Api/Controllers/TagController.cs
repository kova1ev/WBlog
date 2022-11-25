using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WBlog.Admin.Models;
using WBlog.Core.Interfaces;
using AutoMapper;
using WBlog.Core.Domain.Entity;
using WBlog.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using WBlog.Core.Domain;

namespace WBlog.Admin.Controllers;

[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;
    private readonly IMapper _mapper;

    public TagController(ITagService service, IMapper mapper)
    {
        this._tagService = service;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<FilteredData<TagModel>>> Get([FromQuery] TagRequestOptions options)
    {
        var tags = await _tagService.GetTagsAsync(options);
        return Ok(_mapper.Map<FilteredData<TagModel>>(tags));
    }

    [AllowAnonymous]
    [HttpGet("popular/{count:int?}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PopularTagModel>>> GetPupular(int count = 10)
    {
        var tags = await _tagService.GetTagsByPopularityAsync(count);
        return Ok(_mapper.Map<IEnumerable<PopularTagModel>>(tags));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TagModel>> Get(Guid id)
    {
        var tag = await _tagService.GetByIdAsync(id);
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
        return Ok(await _tagService.SaveAsync(tag));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> Put([FromBody] TagModel entity)
    {
        var tag = _mapper.Map<Tag>(entity);
        return Ok(await _tagService.UpdateAsync(tag));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        return Ok(await _tagService.DeleteAsync(id));
    }

    #endregion
}