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
public class SubcategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ISubcategoryService _subcategoryService;
    private readonly IMapper _mapper;

    public SubcategoriesController(ICategoryService categoryService, ISubcategoryService subcategoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _subcategoryService = subcategoryService;
        _mapper = mapper;
    }

    // GET: api/Subcategories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubcategoryDTO>>> Get()
    {
        var subcategories = await _subcategoryService.GetAllSubcategories();

        return Ok(_mapper.Map<List<SubcategoryDTO>>(subcategories));
    }

    // GET: api/Subcategories/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SubcategoryReadDTO>> Get(int id)
    {
        var subcategory = await _subcategoryService.GetSubcategoryById(id);

        if (subcategory == null) return NotFound();

        return Ok(_mapper.Map<SubcategoryReadDTO>(subcategory));
    }

    // PUT: api/Subcategories/5
    // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, SubcategoryUpdateDTO subcategoryIn)
    {
        if (id != subcategoryIn.Id) return BadRequest();

        var subcategory = await _subcategoryService.GetSubcategoryById(id);

        if (subcategory == null) return NotFound();

        _mapper.Map(subcategoryIn, subcategory);

        if (subcategoryIn.CategoriesIds != null)
        {
            subcategory.Categories.Clear();

            await foreach (var category in _categoryService.GetAllCategories(subcategoryIn.CategoriesIds))
            {
                if (category == null) return NotFound("One or more categories does not exists");
                subcategory.Categories.Add(category);
            }
        }

        await _subcategoryService.UpdateSubcategory(subcategory);

        return NoContent();
    }

    // POST: api/Subcategories
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<SubcategoryReadDTO>> Post(SubcategoryCreateDTO subcategoryIn)
    {
        var subcategory = _mapper.Map<Subcategory>(subcategoryIn);

        await _subcategoryService.CreateSubcategory(subcategory);

        return CreatedAtAction(nameof(Get), new { id = subcategory.Id },
            _mapper.Map<SubcategoryReadDTO>(subcategory));
    }

    // DELETE: api/Subcategories/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var subcategory = await _subcategoryService.GetSubcategoryById(id);

        if (subcategory == null) return NotFound();

        await _subcategoryService.DeleteSubcategory(id);

        return NoContent();
    }
}