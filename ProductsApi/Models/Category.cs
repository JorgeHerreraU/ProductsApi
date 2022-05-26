using ProductsApi.Enums;

namespace ProductsApi.Models;

public class Category : BaseEntity
{
    public Category()
    {
        Subcategories = new HashSet<Subcategory>();
        Products = new HashSet<Product>();
    }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Priority Priority { get; set; }
    public ICollection<Subcategory> Subcategories { get; set; }
    public ICollection<Product> Products { get; set; }
}