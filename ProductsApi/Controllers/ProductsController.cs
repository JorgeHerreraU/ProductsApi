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
public class ProductsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICategoryService _categoryService;
    private readonly ISubcategoryService _subcategoryService;
    private readonly IProductService _productService;

    public ProductsController(IMapper mapper, ICategoryService categoryService, ISubcategoryService subcategoryService, IProductService productService)
    {
        _mapper = mapper;
        _categoryService = categoryService;
        _subcategoryService = subcategoryService;
        _productService = productService;
    }

    // GET: api/Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductReadDTO>>> Get()
    {
        var products = await _productService.GetAllProducts();

        return _mapper.Map<List<ProductReadDTO>>(products);
    }


    // GET: api/Products/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductReadDTO>> Get(int id)
    {
        var product = await _productService.GetProductById(id);

        if (product == null) return NotFound();

        return _mapper.Map<ProductReadDTO>(product);
    }

    // PUT: api/Products/5
    // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, ProductUpdateDTO productIn)
    {
        if (id != productIn.Id) return BadRequest();

        var product = await _productService.GetProductById(id);

        if (product == null) return NotFound();

        if (productIn.CategoryId != null)
        {
            var category = await _categoryService.GetCategoryById(productIn.CategoryId.Value);
            if (category == null) return NotFound();
            product.Category = category;
        }

        _mapper.Map(productIn, product);

        if (productIn.SubcategoriesIds != null)
        {
            product.Subcategories!.Clear();

            await foreach (var subcategory in _subcategoryService.GetAllSubcategories(productIn.SubcategoriesIds))
            {
                if (subcategory == null) return NotFound("One or more subcategories does not exists");
                product.Subcategories!.Add(subcategory);
            }
        }

        await _productService.UpdateProduct(product);

        return NoContent();
    }

    // POST: api/Products
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ProductReadDTO>> Post(ProductCreateDTO productIn)
    {
        var product = _mapper.Map<Product>(productIn);

        var category = await _categoryService.GetCategoryById(productIn.CategoryId);

        if (category == null) return NotFound();

        product.Category = category;

        if (productIn.SubcategoriesIds is not null)
        {
            await foreach (var subcategory in _subcategoryService.GetAllSubcategories(productIn.SubcategoriesIds))
            {
                if (subcategory == null) return NotFound("One or more subcategories does not exists");
                product.Subcategories!.Add(subcategory);
            }
        }

        await _productService.CreateProduct(product);

        return CreatedAtAction(nameof(Get), new { id = product.Id }, _mapper.Map<ProductReadDTO>(product));
    }

    // DELETE: api/Products/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetProductById(id);

        if (product == null) return NotFound();

        await _productService.DeleteProduct(id);

        return NoContent();
    }
}