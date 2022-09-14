using AutoMapper;
using WBlog.Core.Domain;
using WBlog.Shared.Models;
using WBlog.Core.Domain.Entity;

namespace WBlog.Api.Mapper;

public class PostMapperProfile : Profile
{
    public PostMapperProfile()
    {
        //posts
        CreateMap<Post, PostDetailsModel>()
            .ForMember(output => output.Tags, src => src.MapFrom(p => p.Tags.Select(t => t.Name)));

        CreateMap<Post, PostIndexModel>();

        CreateMap<FiltredData<Post>, FiltredDataModel<PostIndexModel>>();

        CreateMap<PostEditModel, Post>()
            .ForMember(src => src.Tags,
                tags => tags.MapFrom(p => (p.Tags ?? Array.Empty<string>()).Select(t => new Tag { Name = t })));
    }
}