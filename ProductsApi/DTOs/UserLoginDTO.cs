using System.ComponentModel.DataAnnotations;

namespace ProductsApi.DTOs;

public class UserLoginDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}
