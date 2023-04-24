using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<TestEntity, TestEntityDto>();
    }
}