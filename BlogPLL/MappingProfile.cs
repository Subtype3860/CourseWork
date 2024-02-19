using AutoMapper;
using BlogPLL.ViewModels.Account;
using BlogDAL.Models;

namespace BlogPLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(x => x.BirthDate, opt => opt.MapFrom(c => c.Date))
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));
            CreateMap<LoginViewModel, User>();

            CreateMap<UserEditViewModel, User>();
            CreateMap<User, UserEditViewModel>().ForMember(x=>x.UserId, opt => opt.MapFrom(c => c.Id));

            CreateMap<UserWithFriendExt, User>();
            CreateMap<User, UserWithFriendExt>();
        }
    }
}
