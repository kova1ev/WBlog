using AutoMapper;
using WBlog.Application.Core.Domain;
using WBlog.Shared.Models;
using WBlog.Application.Core.Domain.Entity;

namespace WBlog.Web.Api.Mapper
{
    public class PostMapperProfile : Profile
    {
        public PostMapperProfile()
        {
            //posts
            CreateMap<Post, PostDetailsModel>()
            .ForMember(output => output.Tags, src => src.MapFrom(p => p.Tags.Select(t => t.Name)));
            CreateMap<Post, PostIndexModel>();

            CreateMap<FiltredPosts, FiltredPostsModel>();
            CreateMap<PostEditModel, Post>()
            .ForMember(src => src.Tags, tags => tags.MapFrom(p => p.Tags.Select(t => new Tag { Name = t })));
        }

    }
}
