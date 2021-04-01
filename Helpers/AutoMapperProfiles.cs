using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ProyectoSalud.API.Dtos;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForDetailedDto>()
                // .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name));
            CreateMap<UserForUpdateDto, User>();
            CreateMap<UserForRegisterDto, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName));
            CreateMap<UserForRecoveryDto, UserForRegisterDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UsernameOrEmail))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UsernameOrEmail));
            CreateMap<UserForRecoveryVerifyDto, UserForRegisterDto>();
            CreateMap<UserForRecoveryDto, User>();
            CreateMap<Rol, RolsToListDto>();
            CreateMap<UserForUpdateDto, UserForRegisterDto>();
            // .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.));
            CreateMap<UserForUpdateDto, Telephone>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.CountryCode))
                .ForMember(dest => dest.NationalNumber, opt => opt.MapFrom(src => src.NationalNumber))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number));
            CreateMap<UserForUpdateDto, Location>()
                .ForMember(dest => dest.CountryAddressId, opt => opt.MapFrom(src => src.CountryAddressId))
                .ForMember(dest => dest.CityAddressId, opt => opt.MapFrom(src => src.CityAddressId))
                .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address1))
                .ForMember(dest => dest.Address2, opt => opt.MapFrom(src => src.Address2))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode));
            CreateMap<UserForUpdateDto, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.Nickname));
            CreateMap<User, UserForEnrollDto>()
            .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone.NationalNumber));
        }
    }
}