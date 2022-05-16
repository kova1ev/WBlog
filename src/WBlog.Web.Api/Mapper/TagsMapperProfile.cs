using AutoMapper;
using WBlog.Shared.Models;
using WBlog.Application.Core.Domain.Entity;

namespace WBlog.Web.Api.Mapper
{
    public class TagsMapperProfile : Profile
    {
        public TagsMapperProfile()
        {
            //tags
            CreateMap<Tag, TagModel>();
            CreateMap<Tag, PopularTagModel>()
            .ForMember(o => o.PostCount, param => param.MapFrom(t => t.Posts.Count));
        }
    }
}
