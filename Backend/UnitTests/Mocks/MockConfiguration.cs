using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace UnitTests.Mocks;

public class MockConfiguration : IConfiguration
{
    public string? this[string key] { get => "YourSecretKey"; set { } }
        
    public IEnumerable<IConfigurationSection> GetChildren()
    {
        throw new NotImplementedException();
    }

    public IChangeToken GetReloadToken()
    {
        throw new NotImplementedException();
    }

    public IConfigurationSection GetSection(string key)
    {
        throw new NotImplementedException();
    }
}