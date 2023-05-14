using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity;

public class TokenValidationParamsProvider
{
    public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
    {
        return new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"] ??
                                                                               throw new InvalidOperationException("Invalid access token secret"))),
            ValidAlgorithms = new[] {SecurityAlgorithms.HmacSha256} // validate the algorithm here
        };
    }
}