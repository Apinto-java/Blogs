using AutoMapper;
using Blogs.Application.DTO.BlogPost;
using Blogs.Application.DTO.Comment;
using Blogs.Application.DTO.User;
using Blogs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.MappingProfile
{
    public class BlogsMappingProfile : Profile
    {
        public BlogsMappingProfile() 
        { 
            CreateMap<BlogPost, CreateBlogPostDTO>().ReverseMap();
            CreateMap<BlogPost, UpdateBlogPostDTO>().ReverseMap();
            CreateMap<BlogPost, BlogPostResultDTO>().ReverseMap();

            CreateMap<Comment, CreateCommentDTO>().ReverseMap();
            CreateMap<Comment, UpdateCommentDTO>().ReverseMap();
            CreateMap<Comment, CommentResultDTO>().ReverseMap();

            CreateMap<User, LoginRequestDTO>().ReverseMap()
                .ForMember(x => x.BlogPostCreationUserNavigations, options => options.Ignore())
                .ForMember(x => x.BlogPostUpdateUserNavigations, options => options.Ignore())
                .ForMember(x => x.CommentCreationUserNavigations, options => options.Ignore())
                .ForMember(x => x.CommentUpdateUserNavigations, options => options.Ignore())
                .ForMember(x => x.InverseCreationUserNavigation, options => options.Ignore())
                .ForMember(x => x.InverseUpdateUserNavigation, options => options.Ignore())
                .ForMember(x => x.UpdateDate, options => options.Ignore())
                .ForMember(x => x.CreationDate, options => options.Ignore())
                .ForMember(x => x.CreationUser, options => options.Ignore())
                .ForMember(x => x.CreationUser, options => options.Ignore())
                .ForMember(x => x.Password, options => options.Ignore());


            CreateMap<User, RegisterRequestDTO>().ReverseMap()
                .ForMember(x => x.BlogPostCreationUserNavigations, options => options.Ignore())
                .ForMember(x => x.BlogPostUpdateUserNavigations, options => options.Ignore())
                .ForMember(x => x.CommentCreationUserNavigations, options => options.Ignore())
                .ForMember(x => x.CommentUpdateUserNavigations, options => options.Ignore())
                .ForMember(x => x.InverseCreationUserNavigation, options => options.Ignore())
                .ForMember(x => x.InverseUpdateUserNavigation, options => options.Ignore())
                .ForMember(x => x.UpdateDate, options => options.Ignore())
                .ForMember(x => x.CreationDate, options => options.Ignore())
                .ForMember(x => x.CreationUser, options => options.Ignore())
                .ForMember(x => x.CreationUser, options => options.Ignore())
                .ForMember(x => x.Password, options => options.Ignore()); ;
        }
    }
}
