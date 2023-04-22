using AutoMapper;
using Bravure.Entities;

namespace Bravure.Models.User
{
    public class MapProfile : Profile
    {
        public MapProfile()
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

            CreateMap<ApplicationUser, UpdateUserDto>();
            CreateMap<ApplicationUser, UpdateUserDto>()
                .ForMember(x => x.Password, opt => opt.Ignore());

            CreateMap<UpdateUserDto, ApplicationUser>();
            CreateMap<UpdateUserDto, ApplicationUser>()
                .ForMember(x => x.Roles, opt => opt.Ignore());
        }
    }
}
