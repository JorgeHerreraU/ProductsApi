using ProductsApi.DAL.Repository;
using ProductsApi.Interfaces;
using ProductsApi.Models;

namespace ProductsApi.Services;

public class SubcategoryService : ISubcategoryService
{
    private readonly IRepository<Subcategory> _repository;

    public SubcategoryService(IRepository<Subcategory> repository)
    {
        _repository = repository;
    }


    public async Task<IEnumerable<Subcategory>> GetAllSubcategories()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Subcategory?> GetSubcategoryById(int id)
    {
        return await _repository.GetByIdAsync(id, x => x.Categories);
    }

    public async IAsyncEnumerable<Subcategory?> GetAllSubcategories(IEnumerable<int> subcategoriesIds)
    {
        foreach (var subcategoryId in subcategoriesIds)
        {
            yield return await _repository.GetByIdAsync(subcategoryId);
        }
    }

    public async Task<Subcategory> CreateSubcategory(Subcategory subcategory)
    {
        await _repository.AddAsync(subcategory);
        return subcategory;
    }

    public async Task<Subcategory> UpdateSubcategory(Subcategory subcategory)
    {
        await _repository.UpdateAsync(subcategory);
        return subcategory;
    }

    public async Task<Subcategory> DeleteSubcategory(int id)
    {
        var subcategory = await CheckIfSubcategoryExists(id);
        await _repository.DeleteAsync(subcategory);
        return subcategory;
    }

    private async Task<Subcategory> CheckIfSubcategoryExists(int id)
    {
        return await _repository.GetByIdAsync(id) ?? throw new Exception("Subcategory not found");
    }
}