using Application.Dto;

namespace Application.Interfaces;

public interface ITestService
{
    public TestEntityDto GenerateTestEntity();
}