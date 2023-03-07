using AirLegance.Application.Dto;
using AirLegance.Domain.Entities;
using AutoMapper;

namespace AirLegance.Application.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TestEntity, TestEntityDto>();
        }
    }
}