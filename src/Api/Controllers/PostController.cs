using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WBlog.Shared.Models;
using WBlog.Core.Interfaces;
using WBlog.Core.Domain;
using WBlog.Core;
using AutoMapper;
using WBlog.Core.Domain.Entity;

namespace WBlog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IMapper _mapper;

    public PostController(IPostService service, IMapper mapper)
    {
        _postService = service;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<FiltredDataModel<PostIndexModel>>> Get([FromQuery] ArticleRequestOptions options)
    {
        var posts = await _postService.GetPosts(options);
        return Ok(_mapper.Map<FiltredDataModel<PostIndexModel>>(posts));
    }

    [HttpGet("getpublished")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<FiltredDataModel<PostIndexModel>>> GetPublished(
        [FromQuery] ArticleRequestOptions options)
    {
        options.Publish = true;
        var posts = await _postService.GetPosts(options);
        return Ok(_mapper.Map<FiltredDataModel<PostIndexModel>>(posts));
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PostDetailsModel>> GetById(Guid id)
    {
        var entity = await _postService.GetPostById(id);
        return Ok(_mapper.Map<PostDetailsModel>(entity));
    }

    [AllowAnonymous]
    [HttpGet("{slug}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PostDetailsModel>> GetBySlug(string slug)
    {
        var entity = await _postService.GetPostBySlug(slug);
        if (entity == null)
            return NotFound();
        return Ok(_mapper.Map<PostDetailsModel>(entity));
    }

    [HttpGet("{id:guid}/tags")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TagModel>>> GetPostTags(Guid id)
    {
        var tags = await _postService.GetPostTags(id);
        return Ok(_mapper.Map<IEnumerable<TagModel>>(tags));
    }

    #region

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        return Ok(await _postService.Delete(id));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> AddPost([FromBody] PostEditModel value)
    {
        //TODO продумать сохранение картинок
        var post = _mapper.Map<Post>(value);
        return Ok(await _postService.Save(post));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> UpdatePost([FromBody] PostEditModel value)
    {
        //TODO продумать сохранение картинок
        var post = _mapper.Map<Post>(value);
        return Ok(await _postService.Update(post));
    }

    [HttpPut("{id:guid}/publish")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Publish(Guid id, [FromQuery] bool publish)
    {
        return Ok(await _postService.PublishPost(id, publish));
    }

    #endregion
}