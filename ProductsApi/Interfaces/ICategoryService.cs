using ProductsApi.Models;

namespace ProductsApi.Interfaces;
public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllCategories();
    IAsyncEnumerable<Category?> GetAllCategories(IEnumerable<int> categoriesIds);
    Task<Category?> GetCategoryById(int id);
    Task<Category> CreateCategory(Category category);
    Task<Category> UpdateCategory(Category category);
    Task<Category> DeleteCategory(int id);
}
