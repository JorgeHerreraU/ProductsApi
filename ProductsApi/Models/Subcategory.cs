using ProductsApi.Enums;

namespace ProductsApi.Models;

public class Subcategory : BaseEntity
{
    public Subcategory()
    {
        Categories = new HashSet<Category>();
        Products = new HashSet<Product>();
    }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Priority Priority { get; set; }
    public ICollection<Category> Categories { get; set; }
    public ICollection<Product>? Products { get; set; }
}