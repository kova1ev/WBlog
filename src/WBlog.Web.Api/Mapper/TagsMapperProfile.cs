using AutoMapper;
using WBlog.Shared.Models;
using WBlog.Application.Core.Domain.Entity;
using WBlog.Application.Core.Domain;

namespace WBlog.Web.Api.Mapper
{
    public class TagsMapperProfile : Profile
    {
        public TagsMapperProfile()
        {
            //tags
            CreateMap<Tag, TagModel>().ReverseMap();
            CreateMap<Tag, PopularTagModel>()
            .ForMember(o => o.PostCount, param => param.MapFrom(t => t.Posts.Count));

            CreateMap<FiltredData<Tag>, FiltredDataModel<TagModel>>();
        }
    }
}
