namespace ProductsApi.DTOs;

public class UserReadDTO
{
    public int Id { get; set; }
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Token { get; set; } = null!;
    public string? RefreshToken { get; set; } = null!;
    public string Role { get; set; } = null!;
}