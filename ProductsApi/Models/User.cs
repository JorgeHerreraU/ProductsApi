using ProductsApi.Enums;

namespace ProductsApi.Models;

public class User : BaseEntity
{
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Role Role { get; set; } = Role.User;
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}