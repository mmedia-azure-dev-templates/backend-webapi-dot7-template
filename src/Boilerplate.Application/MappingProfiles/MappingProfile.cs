using AutoMapper;
using Boilerplate.Application.Features.Heroes;
using Boilerplate.Application.Features.Heroes.CreateHero;
using Boilerplate.Application.Features.Heroes.UpdateHero;
using Boilerplate.Application.Features.Users;
using Boilerplate.Domain.Entities;

namespace Boilerplate.Application.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ApplicationUser Map
        CreateMap<ApplicationUser, GetUserResponse>().ForMember(dest => dest.Message, opt => opt.MapFrom(org => org.Email));
        //CreateMap<CreateUserRequest, User>().ForMember(dest => dest.Role,
        //    opt => opt.MapFrom(org => org.IsAdmin ? Roles.Admin : Roles.User));
        //CreateMap<UpdatePasswordRequest, User>();

        // Hero Map
        CreateMap<Hero, GetHeroResponse>().ReverseMap();
        CreateMap<CreateHeroRequest, Hero>();
        CreateMap<UpdateHeroRequest, Hero>();

        
    }
}