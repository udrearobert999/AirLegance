using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class TestService : ITestService
{
    private readonly IMapper _mapper;

    public TestService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TestEntityDto GenerateTestEntity()
    {
        var testEntity = new TestEntity("Test", "Test");

        return _mapper.Map<TestEntityDto>(testEntity);
    }
}