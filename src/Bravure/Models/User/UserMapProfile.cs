using AutoMapper;
using Bravure.Entities;
using Bravure.Models.User;

namespace Bravure.Models.User
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<UserDto, ApplicationUser>();
            CreateMap<UserDto, ApplicationUser>()
                .ForMember(x => x.Roles, opt => opt.Ignore());

            CreateMap<CreateUserDto, ApplicationUser>();
            CreateMap<CreateUserDto, ApplicationUser>().ForMember(x => x.Roles, opt => opt.Ignore());

            CreateMap<ApplicationUser, UserDto>();
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(x => x.Password, opt => opt.Ignore());


            CreateMap<ApplicationUser, CreateUserDto>();
            CreateMap<ApplicationUser, CreateUserDto>()
                .ForMember(x => x.Password, opt => opt.Ignore());
        }
    }
}
