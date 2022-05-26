using ProductsApi.Models;
using System.Text.Json.Serialization;

namespace ProductsApi.DTOs;

public class ProductUpdateDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    [JsonPropertyName("category")]
    public int? CategoryId { get; set; }
    [JsonPropertyName("subcategories")]
    public IEnumerable<int>? SubcategoriesIds { get; set; } = null!;

}
