using ProductsApi.Models;

namespace ProductsApi.Interfaces;

public interface ISubcategoryService
{
    Task<IEnumerable<Subcategory>> GetAllSubcategories();
    Task<Subcategory?> GetSubcategoryById(int id);
    IAsyncEnumerable<Subcategory?> GetAllSubcategories(IEnumerable<int> subcategoriesIds);
    Task<Subcategory> CreateSubcategory(Subcategory subcategory);
    Task<Subcategory> UpdateSubcategory(Subcategory subcategory);
    Task<Subcategory> DeleteSubcategory(int id);
}