using System.ComponentModel.DataAnnotations;

namespace ProductsApi.DTOs;

public class UserUpdateDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Firstname { get; set; } = null!;
    [Required]
    public string Lastname { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Role { get; set; } = null!;
}
