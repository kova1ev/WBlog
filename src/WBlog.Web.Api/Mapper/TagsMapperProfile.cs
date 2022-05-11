using AutoMapper;
using WBlog.Application.Core.Dto;
using WBlog.Application.Domain.Entity;

namespace WBlog.Web.Api.Mapper
{
    public class TagsMapperProfile : Profile
    {
        public TagsMapperProfile()
        {
            //tags
            CreateMap<Tag, TagDto>();
            CreateMap<Tag, PopularTagDto>()
            .ForMember(o => o.PostCount, param => param.MapFrom(t => t.Posts.Count));
        }
    }
}
