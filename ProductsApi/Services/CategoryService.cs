using ProductsApi.DAL.Repository;
using ProductsApi.Interfaces;
using ProductsApi.Models;

namespace ProductsApi.Services;

public class CategoryService: ICategoryService
{
    private readonly IRepository<Category> _repository;

    public CategoryService(IRepository<Category> repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await _repository.GetAllAsync();
    }

    public async IAsyncEnumerable<Category?> GetAllCategories(IEnumerable<int> categoriesIds)
    {
        foreach (var categoryId in categoriesIds)
        {
            yield return await _repository.GetByIdAsync(categoryId);
        }
    }

    public async Task<Category?> GetCategoryById(int id)
    {
        return await _repository.GetByIdAsync(id, x => x.Subcategories);
    }

    public async Task<Category> CreateCategory(Category category)
    {
        await _repository.AddAsync(category);
        return category;
    }

    public async Task<Category> UpdateCategory(Category category)
    {
        await _repository.UpdateAsync(category);
        return category;
    }

    public async Task<Category> DeleteCategory(int id)
    {
        var category = await CheckIfCategoryExists(id);
        await _repository.DeleteAsync(category);
        return category;
    }

    private async Task<Category> CheckIfCategoryExists(int id)
    {
        return await _repository.GetByIdAsync(id) ?? throw new Exception("Category not found");
    }
}
