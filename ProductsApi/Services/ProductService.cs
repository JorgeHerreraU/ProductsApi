using ProductsApi.DAL.Repository;
using ProductsApi.Interfaces;
using ProductsApi.Models;

namespace ProductsApi.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _repository;

    public ProductService(IRepository<Product> repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _repository.GetAllAsync(product => product.Category, product => product.Subcategories!);
    }

    public async Task<Product?> GetProductById(int id)
    {
        return await _repository.GetByIdAsync(id, product => product.Category, product => product.Subcategories!);
    } 

    public async Task<Product> CreateProduct(Product product)
    {
        await _repository.AddAsync(product);
        return product;
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        await _repository.UpdateAsync(product);
        return product;
    }

    public async Task<Product> DeleteProduct(int id)
    {
        var product = await CheckIfProductExists(id);
        await _repository.DeleteAsync(product);
        return product;
    }

    private async Task<Product> CheckIfProductExists(int id)
    {
        return await _repository.GetByIdAsync(id) ?? throw new Exception("Product not found");
    }
}