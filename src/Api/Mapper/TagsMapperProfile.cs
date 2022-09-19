using AutoMapper;
using WBlog.Api.Models;
using WBlog.Core.Domain.Entity;
using WBlog.Core.Domain;

namespace WBlog.Api.Mapper;

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