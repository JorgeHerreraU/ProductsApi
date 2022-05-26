using ProductsApi.Enums;

namespace ProductsApi.DTOs;

public class CategoryCreateDTO
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Priority Priority { get; set; }

}