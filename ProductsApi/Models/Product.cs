namespace ProductsApi.Models;

public class Product : BaseEntity
{
    public Product()
    {
        Category = new Category();
        Subcategories = new HashSet<Subcategory>();
    }
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<Subcategory>? Subcategories { get; set; }
}