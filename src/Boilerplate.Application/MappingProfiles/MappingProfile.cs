using AutoMapper;
using Boilerplate.Application.Features.Articles.ArticleCreate;
using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Application.Features.Heroes;
using Boilerplate.Application.Features.Heroes.CreateHero;
using Boilerplate.Application.Features.Heroes.UpdateHero;
using Boilerplate.Application.Features.Users.GetUserById;
using Boilerplate.Application.Features.Users.GetUserByToken;
using Boilerplate.Application.Features.Users.GetUsers;
using Boilerplate.Domain.Entities;

namespace Boilerplate.Application.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User Map
        CreateMap<(ApplicationUser applicationUser, UserInformation userInformation), GetUserByIdResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.applicationUser.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.userInformation.UserId))
            .ForMember(dest => dest.Ndocument, opt => opt.MapFrom(src => src.userInformation.Ndocument))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.applicationUser.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.applicationUser.LastName))
            .ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => src.applicationUser.LastLogin))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.applicationUser.Email))
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.applicationUser.EmailConfirmed))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.userInformation.BirthDate))
            .ForMember(dest => dest.EntryDate, opt => opt.MapFrom(src => src.userInformation.EntryDate))
            .ForMember(dest => dest.DepartureDate, opt => opt.MapFrom(src => src.userInformation.DepartureDate))
            .ForMember(dest => dest.Hired, opt => opt.MapFrom(src => src.userInformation.Hired))
            .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.userInformation.ImgUrl))
            .ForMember(dest => dest.CurriculumUrl, opt => opt.MapFrom(src => src.userInformation.CurriculumUrl))
            .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.userInformation.Mobile))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.userInformation.Phone))
            .ForMember(dest => dest.PrimaryStreet, opt => opt.MapFrom(src => src.userInformation.PrimaryStreet))
            .ForMember(dest => dest.SecondaryStreet, opt => opt.MapFrom(src => src.userInformation.SecondaryStreet))
            .ForMember(dest => dest.Numeration, opt => opt.MapFrom(src => src.userInformation.Numeration))
            .ForMember(dest => dest.Reference, opt => opt.MapFrom(src => src.userInformation.Reference))
            .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.userInformation.Provincia))
            .ForMember(dest => dest.Canton, opt => opt.MapFrom(src => src.userInformation.Canton))
            .ForMember(dest => dest.Parroquia, opt => opt.MapFrom(src => src.userInformation.Parroquia))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.userInformation.Notes))
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.userInformation.DateCreated))
            .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => src.userInformation.DateUpdated))
            ;

        // User Map
        CreateMap<(ApplicationUser applicationUser, UserInformation userInformation), GetUserByTokenResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.applicationUser.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.userInformation.UserId))
            .ForMember(dest => dest.Ndocument, opt => opt.MapFrom(src => src.userInformation.Ndocument))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.applicationUser.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.applicationUser.LastName))
            .ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => src.applicationUser.LastLogin))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.applicationUser.Email))
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.applicationUser.EmailConfirmed))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.userInformation.BirthDate))
            .ForMember(dest => dest.EntryDate, opt => opt.MapFrom(src => src.userInformation.EntryDate))
            .ForMember(dest => dest.DepartureDate, opt => opt.MapFrom(src => src.userInformation.DepartureDate))
            .ForMember(dest => dest.Hired, opt => opt.MapFrom(src => src.userInformation.Hired))
            .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.userInformation.ImgUrl))
            .ForMember(dest => dest.CurriculumUrl, opt => opt.MapFrom(src => src.userInformation.CurriculumUrl))
            .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.userInformation.Mobile))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.userInformation.Phone))
            .ForMember(dest => dest.PrimaryStreet, opt => opt.MapFrom(src => src.userInformation.PrimaryStreet))
            .ForMember(dest => dest.SecondaryStreet, opt => opt.MapFrom(src => src.userInformation.SecondaryStreet))
            .ForMember(dest => dest.Numeration, opt => opt.MapFrom(src => src.userInformation.Numeration))
            .ForMember(dest => dest.Reference, opt => opt.MapFrom(src => src.userInformation.Reference))
            .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.userInformation.Provincia))
            .ForMember(dest => dest.Canton, opt => opt.MapFrom(src => src.userInformation.Canton))
            .ForMember(dest => dest.Parroquia, opt => opt.MapFrom(src => src.userInformation.Parroquia))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.userInformation.Notes))
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.userInformation.DateCreated))
            .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => src.userInformation.DateUpdated))
            ;

        // Hero Map
        CreateMap<Article, ArticleSearchResponse>();
        CreateMap<Article, ArticleSearchResponse>().ReverseMap();
        CreateMap<Hero, GetHeroResponse>().ReverseMap();
        CreateMap<CreateHeroRequest, Hero>();
        CreateMap<UpdateHeroRequest, Hero>();

        
    }
}