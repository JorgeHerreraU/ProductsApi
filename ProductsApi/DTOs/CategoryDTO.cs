using ProductsApi.Enums;

namespace ProductsApi.DTOs;

public class CategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Priority { get; set; } = null!;
}
