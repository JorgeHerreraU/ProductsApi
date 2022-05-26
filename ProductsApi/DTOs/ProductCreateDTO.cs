using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductsApi.DTOs;

public class ProductCreateDTO
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public double Price { get; set; }
    [JsonPropertyName("category")]
    [Required]
    public int CategoryId { get; set; }
    [JsonPropertyName("subcategories")]
    public IEnumerable<int>? SubcategoriesIds { get; set; } = null!;
}
