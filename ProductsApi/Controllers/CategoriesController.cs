using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsApi.DTOs;
using ProductsApi.Interfaces;
using ProductsApi.Models;

namespace ProductsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ISubcategoryService _subcategoryService;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, ISubcategoryService subcategoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _subcategoryService = subcategoryService;
        _mapper = mapper;
    }

    // GET: api/Categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
    {
        var categories = await _categoryService.GetAllCategories();

        return Ok(_mapper.Map<List<CategoryDTO>>(categories));
    }

    // GET api/Categories/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryReadDTO>> Get(int id)
    {
        var category = await _categoryService.GetCategoryById(id);

        if (category == null) return NotFound();

        return Ok(_mapper.Map<CategoryReadDTO>(category));
    }

    // POST: api/Categories
    [HttpPost]
    public async Task<ActionResult<CategoryReadDTO>> Post(CategoryCreateDTO categoryIn)
    {
        var category = _mapper.Map<Category>(categoryIn);

        await _categoryService.CreateCategory(category);

        return CreatedAtAction(nameof(Get), new { id = category.Id }, _mapper.Map<CategoryReadDTO>(category));
    }

    // PUT api/Categories/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, CategoryUpdateDTO categoryIn)
    {
        if (id != categoryIn.Id) return BadRequest();

        var category = await _categoryService.GetCategoryById(id);

        if (category == null) return NotFound();

        _mapper.Map(categoryIn, category);

        if (categoryIn.SubcategoriesIds != null)
        {
            category.Subcategories.Clear();

            await foreach (var subcategory in _subcategoryService.GetAllSubcategories(categoryIn.SubcategoriesIds))
            {
                if (subcategory == null) return NotFound("One or more subcategories does not exists");
                category.Subcategories.Add(subcategory);
            }
        }

        await _categoryService.UpdateCategory(category);

        return NoContent();
    }

    // DELETE api/Categories/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryService.GetCategoryById(id);

        if (category == null) return NotFound();

        await _categoryService.DeleteCategory(id);

        return NoContent();
    }
}