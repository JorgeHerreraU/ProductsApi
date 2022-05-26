using ProductsApi.Enums;

namespace ProductsApi.DTOs;

public class SubcategoryReadDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Priority { get; set; } = null!;
    public List<CategoryDTO> Categories { get; set; } = null!;
}
