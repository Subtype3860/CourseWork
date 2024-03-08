using AutoMapper;
using BlogBLL.ViewModels.Account;
using BlogBLL.ViewModels.Comment;
using BlogBLL.ViewModels.Post;
using BlogBLL.ViewModels.Tag;
using BlogBLL.ViewModels.User;
using BlogDAL.Models;

namespace BlogBLL;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //MappingUser
        CreateMap<UserEditViewModel, User>()
            .ForMember(x=>x.Id, opt =>
                opt.MapFrom(c=>c.UserId));
        CreateMap<UserEditViewModel, User>()
            .ForMember(x => x.BirthDate, opt =>
                opt.MapFrom(c => c.BirthDate));
        CreateMap<User, UserEditViewModel>()
            .ForMember(x=>x.UserId, opt => 
                opt.MapFrom(c => c.Id));
        
        CreateMap<User, UserViewModel>()
            .ForMember(x => x.UserId, opt =>
                opt.MapFrom(c => c.Id));
        CreateMap<UserViewModel, User>()
            .ForMember(x => x.Id, opt =>
                opt.MapFrom(c => c.UserId));
        
        //MappingPost
        CreateMap<EditPostViewModel, Post>()
            .ForMember(x => x.Body, opt =>
                opt.MapFrom(c => c.Post))
            .ForMember(x => x.Id, opt =>
                opt.MapFrom(c => c.PostId));
        
        CreateMap<Post, EditPostViewModel>()
            .ForMember(x => x.Post, opt =>
                opt.MapFrom(c => c.Body))
            .ForMember(x => x.PostId, opt =>
                opt.MapFrom(c => c.Id));
        
        
        CreateMap<GetPostViewModel, Post>();
        CreateMap<Post, GetPostViewModel>();
        CreateMap<AddPostViewModel, Post>()
            .ForMember(x => x.Body, opt =>
                opt.MapFrom(c => c.Post))
            .ForMember(x=>x.Id,opt=>
                opt.MapFrom(c=>c.PostId))
            .ForMember(x=>x.DatePublic, opt=>
                opt.MapFrom(c=>c.Public));
        
        CreateMap<Post, AddPostViewModel>()
            .ForMember(x => x.Post, opt =>
                opt.MapFrom(c => c.Body))
            .ForMember(x=>x.PostId,opt=>
                opt.MapFrom(c=>c.Id))
            .ForMember(x=>x.Public, opt=>
                opt.MapFrom(c=>c.DatePublic
                ));
        
        //MappingCommint
        CreateMap<AddCommentViewModel, Remark>();
        CreateMap<Remark, AddCommentViewModel>();
        CreateMap<EditCommentViewModel, Remark>();
        CreateMap<Remark, EditCommentViewModel>();
        
        //MappingTag
        CreateMap<AddTagViewModel, Tag>();
        CreateMap<Tag, AddTagViewModel>();
        CreateMap<Tag, EditTagViewModel>();
        CreateMap<EditTagViewModel, Tag>();
    }
}