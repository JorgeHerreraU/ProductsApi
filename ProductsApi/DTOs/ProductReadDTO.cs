using ProductsApi.Models;

namespace ProductsApi.DTOs;

public class ProductReadDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public CategoryDTO Category { get; set; } = null!;
    public List<SubcategoryDTO> Subcategories{ get; set; } = null!;
}
