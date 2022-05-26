using System.ComponentModel.DataAnnotations;

namespace ProductsApi.DTOs;

public class UserRegisterDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
