using System.Text.Json.Serialization;
using ProductsApi.Enums;
using ProductsApi.Models;

namespace ProductsApi.DTOs;

public class SubcategoryCreateDTO
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Priority Priority { get; set; }
}
