using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Config;

public class TokenValidationParametersConfiguration
{
    public static TokenValidationParameters GetTokenValidationParameters(string jwtSecret)
    {
        return new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 }
        };
    }
}