using System;
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
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Person.BirthDate.CalculateAge()))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Person.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Person.LastName));
            CreateMap<UserForUpdateDto, User>();
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Person.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Person.LastName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Person.Gender))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Person.BirthDate.CalculateAge()))
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Person.PhotoUrl));
            CreateMap<UserForRegisterDto, User>();
            CreateMap<UserForRecoveryDto, UserForRegisterDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UsernameOrEmail))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UsernameOrEmail));
            CreateMap<UserForRecoveryVerifyDto, UserForRegisterDto>();
            CreateMap<UserForRecoveryDto, User>();
            CreateMap<Rol, RolsToListDto>();
            CreateMap<UserForUpdateDto, UserForRegisterDto>();
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
                .ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.Nickname));
            CreateMap<PatientRegistrationDto, Person>();
            CreateMap<PatientRegistrationDto, Insure>();
            CreateMap<ConsultationForCreationDto, Consultation>()
                .ForMember(dest => dest.UpdateUserId, opt => opt.MapFrom(src => src.CreationUserId))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<Person, PatientToListDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Insure.Type))
                .ForMember(dest => dest.RegistrationNumber, opt => opt.MapFrom(src => src.Insure.RegistrationNumber));
            CreateMap<Person, PatientToReturnDto>()
                .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone.Number))
                .ForMember(dest => dest.CellPhone, opt => opt.MapFrom(src => src.CellPhone.Number))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Insure.Type))
                .ForMember(dest => dest.RegistrationNumber, opt => opt.MapFrom(src => src.Insure.RegistrationNumber))
                .ForMember(dest => dest.Kinship, opt => opt.MapFrom(src => src.Insure.Kinship))
                .ForMember(dest => dest.Observations, opt => opt.MapFrom(src => src.Insure.Observations))
                .ForMember(dest => dest.PathologicalBackground, opt => opt.MapFrom(src => src.Insure.PathologicalBackground));
        }
    }
}