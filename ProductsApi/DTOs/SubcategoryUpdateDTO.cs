using System.Text.Json.Serialization;
using ProductsApi.Enums;

namespace ProductsApi.DTOs;

public class SubcategoryUpdateDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Priority Priority { get; set; }
    [JsonPropertyName("categories")]
    public IEnumerable<int>? CategoriesIds { get; set; }
}
