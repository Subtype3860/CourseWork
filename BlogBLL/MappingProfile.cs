using AutoMapper;
using BlogBLL.ViewModels.Account;
using BlogBLL.ViewModels.Comment;
using BlogBLL.ViewModels.Post;
using BlogBLL.ViewModels.Tag;
using BlogDAL.Models;

namespace BlogBLL;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //MappingUser
        CreateMap<RegisterViewModel, User>()
            .ForMember(x => x.Email, opt => 
                opt.MapFrom(c => c.EmailReg))
            .ForMember(x => x.UserName, opt => 
                opt.MapFrom(c => c.Login));
        CreateMap<EditViewModel, User>()
            .ForMember(x=>x.Id, opt =>
                opt.MapFrom(c=>c.UserId));
        CreateMap<User, EditViewModel>()
            .ForMember(x=>x.UserId, opt => 
                opt.MapFrom(c => c.Id));
        //MappingPost
        CreateMap<EditPostViewModel, Post>()
            .ForMember(x => x.Body, opt =>
                opt.MapFrom(c => c.Post))
            .ForMember(x => x.Id, opt =>
                opt.MapFrom(c => c.PostId));
        CreateMap<GetPostViewModel, Post>();
        CreateMap<Post, GetPostViewModel>();
        //MappingCommint
        CreateMap<AddCommentViewModel, Commentaries>();
        CreateMap<Commentaries, AddCommentViewModel>();
        CreateMap<EditCommentViewModel, Commentaries>();
        CreateMap<Commentaries, EditCommentViewModel>();
        //MappingTag
        CreateMap<AddTagViewModel, Tag>();
        CreateMap<Tag, AddTagViewModel>();
        CreateMap<EditTagViewModel, Tag>();
        CreateMap<Tag, EditTagViewModel>();
    }
}