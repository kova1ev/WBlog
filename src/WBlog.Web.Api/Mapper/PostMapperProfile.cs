using AutoMapper;
using WBlog.Application.Core;
using WBlog.Application.Core.Dto;
using WBlog.Application.Domain.Entity;

namespace WBlog.Web.Api.Mapper
{
    public class PostMapperProfile : Profile
    {
        public PostMapperProfile()
        {
            //posts
            CreateMap<Post, PostDetailsDto>()
            .ForMember(output => output.Tags, src => src.MapFrom(p => p.Tags.Select(t => t.Name)));
            CreateMap<Post, PostIndexDto>();

            CreateMap<FiltredPosts, FiltredPostsDto>();
        }

    }
}
