using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProductsApi.Enums;

namespace ProductsApi.DTOs;

public class UserCreateDTO
{
    [Required]
    public string Firstname { get; set; } = null!;
    [Required]
    public string Lastname { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Role { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}