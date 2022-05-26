using System.ComponentModel.DataAnnotations;

namespace ProductsApi.DTOs;

public class RefreshTokenDTO
{
    [Required]
    public string Token { get; set; } = null!;
    [Required]
    public string RefreshToken { get; set; } = null!;
}
