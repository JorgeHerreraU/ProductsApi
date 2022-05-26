using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductsApi.Interfaces;
using ProductsApi.Models;
using ProductsApi.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductsApi.Services;

public class AuthService : IAuthService
{
    private const double EXPIRY_DURATION_MINUTES = 120;
    private readonly AppIdentitySettings _settings;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public AuthService(IOptions<AppIdentitySettings> settings, JwtSecurityTokenHandler tokenHandler)
    {
        _settings = settings.Value;
        _tokenHandler = tokenHandler;
    }

    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }

    public string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _settings.Audience,
            Issuer = _settings.Issuer,
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            }),
            Expires = DateTime.UtcNow.AddMinutes(EXPIRY_DURATION_MINUTES),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
        };
        
        var token = _tokenHandler.CreateToken(tokenDescriptor);

        return _tokenHandler.WriteToken(token);
    }

    public string? GetEmailFromToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key)),
            ValidateLifetime = false
        };
        var claimsPrincipal = _tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        var invalidToken = securityToken is not JwtSecurityToken;
        var invalidTokenAlgorithm = ((JwtSecurityToken)securityToken).Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase) is not true;
        
        if (invalidToken || invalidTokenAlgorithm)
        {
            throw new SecurityTokenException("Invalid token");
        }
        
        return claimsPrincipal?.FindFirstValue(ClaimTypes.Email);
    }
}
