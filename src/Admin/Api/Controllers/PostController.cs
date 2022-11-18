using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WBlog.Admin.Models;
using WBlog.Core.Interfaces;
using WBlog.Core.Domain;
using WBlog.Core;
using AutoMapper;
using WBlog.Core.Domain.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using WBlog.Api.Models;

namespace WBlog.Admin.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IMapper _mapper;

    public PostController(IPostService service, IMapper mapper)
    {
        _postService = service;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<FiltredData<ArticleIndexApiModel>>> Get([FromQuery] ArticleRequestOptions options)
    {
        var posts = await _postService.GetPostsAsync(options);
        return Ok(_mapper.Map<FiltredData<ArticleIndexApiModel>>(posts));
    }
    [AllowAnonymous]
    [HttpGet("getpublished")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<FiltredData<ArticleIndexApiModel>>> GetPublished(
        [FromQuery] ArticleRequestOptions options)
    {
        options.Publish = true;
        var posts = await _postService.GetPostsAsync(options);
        return Ok(_mapper.Map<FiltredData<ArticleIndexApiModel>>(posts));
    }


    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ArticleFullModel>> GetById(Guid id)
    {
        var entity = await _postService.GetPostByIdAsync(id);
        return Ok(_mapper.Map<ArticleFullModel>(entity));
    }

    [AllowAnonymous]
    [HttpGet("{slug}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ArticleFullModel>> GetBySlug(string slug)
    {
        var entity = await _postService.GetPostBySlugAsync(slug);
        if (entity == null)
            return NotFound();
        return Ok(_mapper.Map<ArticleFullModel>(entity));
    }

    [HttpGet("{id:guid}/tags")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TagModel>>> GetPostTags(Guid id)
    {
        var tags = await _postService.GetPostTagsAsync(id);
        return Ok(_mapper.Map<IEnumerable<TagModel>>(tags));
    }

    #region

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        return Ok(await _postService.DeleteAsync(id));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> AddPost([FromBody] ArticleEditModel value)
    {
        //TODO продумать сохранение картинок
        var post = _mapper.Map<Post>(value);
        return Ok(await _postService.SaveAsync(post));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> UpdatePost([FromBody] ArticleEditModel value)
    {
        //TODO продумать сохранение картинок
        var post = _mapper.Map<Post>(value);
        return Ok(await _postService.UpdateAsync(post));
    }

    [HttpPut("{id:guid}/publish")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Publish(Guid id, [FromQuery] bool publish)
    {
        return Ok(await _postService.PublishPostAsync(id, publish));
    }

    #endregion
}