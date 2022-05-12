using AutoMapper;
using WBlog.Shared.Dto;
using WBlog.Application.Core.Entity;

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
