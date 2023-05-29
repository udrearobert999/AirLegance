using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<UserRegistrationRequestDto, User>();
        CreateMap<User, UserRegistrationResponseDto>();

        CreateMap<User, UserAuthResponseDto>();

        CreateMap<Location, LocationDto>().ReverseMap();
        CreateMap<FlightSearchRequestDto, Flight>();


        CreateMap<Flight, FlightSearchResponseDto>()
            .ForMember(
                dest => dest.Source,
                opt => opt.MapFrom(src => $"{src.SourceLocation.City} ({src.SourceLocation.Country})")
            )
            .ForMember(
                dest => dest.Destination,
                opt => opt.MapFrom(src => $"{src.DestinationLocation.City} ({src.DestinationLocation.Country})")
            );
    }
}