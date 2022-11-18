using AutoMapper;
using WBlog.Core.Domain;
using WBlog.Admin.Models;
using WBlog.Core.Domain.Entity;
using WBlog.Api.Models;

namespace WBlog.Admin.Mapper;

public class PostMapperProfile : Profile
{
    public PostMapperProfile()
    {
        //posts
        CreateMap<Post, ArticleFullModel>()
            .ForMember(output => output.Tags, src => src.MapFrom(p => p.Tags.Select(t => t.Name)));

        CreateMap<Post, ArticleIndexApiModel>();

        CreateMap<FiltredData<Post>, FiltredData<ArticleIndexApiModel>>();

        CreateMap<ArticleEditModel, Post>()
            .ForMember(src => src.Tags,
                tags => tags.MapFrom(p => (p.Tags ?? Array.Empty<string>()).Select(t => new Tag { Name = t })));
    }
}