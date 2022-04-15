using AutoMapper;
using WBlog.Application.Core.Dto;
using WBlog.Application.Domain.Entity;

namespace WBlog.Infrastructure.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //posts
            CreateMap<Post, PostDetailsDto>()
            .ForMember(output => output.Tags, src => src.MapFrom(p => p.Tags.Select(t => t.Name)));
            CreateMap<Post, PostIndexDto>();

            //tags
            CreateMap<Tag, TagDto>();
            CreateMap<Tag, PopularTagDto>()
            .ForMember(o => o.PostCount, param => param.MapFrom(t => t.Posts.Count));

        }

    }
}