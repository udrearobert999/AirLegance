using AirLegance.Application.Dto;
using AirLegance.Application.Interfaces;
using AirLegance.Domain.Entities;
using AutoMapper;

namespace AirLegance.Application.Services
{
    public class TestTestService : ITestService
    {
        private readonly IMapper _mapper;

        public TestTestService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TestEntityDto GenerateTestEntity()
        {
            var testEntity = new TestEntity("Test", "Test");

            return _mapper.Map<TestEntityDto>(testEntity);
        }
    }
}
