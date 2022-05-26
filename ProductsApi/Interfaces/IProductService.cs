using ProductsApi.Models;

namespace ProductsApi.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product?> GetProductById(int id);
    Task<Product> CreateProduct(Product product);
    Task<Product> UpdateProduct(Product product);
    Task<Product> DeleteProduct(int id);
}