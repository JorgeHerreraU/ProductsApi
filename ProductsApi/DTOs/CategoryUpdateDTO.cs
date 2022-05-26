using System.Text.Json.Serialization;
using ProductsApi.Enums;

namespace ProductsApi.DTOs;

public class CategoryUpdateDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Priority Priority { get; set; }
    [JsonPropertyName("subcategories")]
    public IEnumerable<int>? SubcategoriesIds { get; set; }
}
