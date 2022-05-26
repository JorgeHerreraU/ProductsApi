using System.Security.Claims;
using ProductsApi.Models;

namespace ProductsApi.Interfaces;

public interface IAuthService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
    string? GetEmailFromToken(string token);
}
