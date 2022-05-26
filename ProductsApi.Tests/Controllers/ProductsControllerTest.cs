using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductsApi.Controllers;
using ProductsApi.DTOs;
using ProductsApi.Interfaces;
using ProductsApi.Models;
using ProductsApi.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsApi.Tests.Controllers;
[TestClass]
public class ProductsControllerTest
{
    private readonly ProductsController _controller;
    private readonly Mock<IProductService> _mockProductService;
    private readonly Mock<ICategoryService> _mockCategoryService;
    private readonly Mock<ISubcategoryService> _mockSubcategoryService;
    private readonly IMapper _mapper;

    public ProductsControllerTest()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProductsProfile>();
            cfg.AddProfile<CategoriesProfile>();
            cfg.AddProfile<SubcategoriesProfile>();
        });
        _mapper = config.CreateMapper();
        _mockProductService = new Mock<IProductService>();
        _mockCategoryService = new Mock<ICategoryService>();
        _mockSubcategoryService = new Mock<ISubcategoryService>();
        _controller = new ProductsController(_mapper, _mockCategoryService.Object, _mockSubcategoryService.Object, _mockProductService.Object);
    }

    [TestMethod]
    public async Task GetProducts_ReturnsAllProducts()
    {
        // Arrange
        _mockProductService.Setup(x => x.GetAllProducts()).ReturnsAsync(SeedData.GetProductsSample());

        // Act
        var result = await _controller.Get();
        var products = result.GetObjectResult();

        // Assert
        Assert.AreEqual(products.Count(), SeedData.GetProductsSample().Count);
    }

    [TestMethod]
    public async Task GetProduct_ReturnsProductReadDTO()
    {
        // Arrange
        _mockProductService.Setup(x => x.GetProductById(1)).ReturnsAsync(SeedData.GetProductSample());

        // Act
        var result = await _controller.Get(1);
        var product = result.GetObjectResult();

        // Assert
        Assert.AreEqual(product.Id, SeedData.GetProductSample().Id);
        Assert.IsInstanceOfType(product, typeof(ProductReadDTO));
    }

    [TestMethod]
    public async Task GetProduct_ReturnsNotFound()
    {
        // Arrange
        _mockProductService.Setup(x => x.GetProductById(1)).ReturnsAsync((Product)null!);

        // Act
        var result = await _controller.Get(1);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task PutProduct_ReturnsBadRequest()
    {
        // Arrange
        var mockData = new ProductUpdateDTO { Id = 2, Name = "Product1", Price = 0, CategoryId = 1, SubcategoriesIds = new List<int> { 1, 2 } };

        // Act
        var result = await _controller.Put(1, mockData);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
    }

    [TestMethod]
    public async Task PutProduct_ReturnsNotFound()
    {
        // Arrange
        var mockData = new ProductUpdateDTO { Id = 1, Name = "Product1", Price = 0, CategoryId = 1, SubcategoriesIds = new List<int> { 1, 2 } };
        _mockProductService.Setup(x => x.GetProductById(1)).ReturnsAsync((Product)null!);

        // Act
        var result = await _controller.Put(1, mockData);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task PutProduct_NullSubcategory_ReturnsNotFound()
    {
        // Arrange
        var mockData = new ProductUpdateDTO { Id = 1, Name = "Product1", Price = 0, CategoryId = 1, SubcategoriesIds = new List<int> { 1, 2 } };
        var mockSubcategories = new List<Subcategory?> { new Subcategory(), null };
        _mockProductService.Setup(x => x.GetProductById(1)).ReturnsAsync(SeedData.GetProductSample());
        _mockCategoryService.Setup(x => x.GetCategoryById(mockData.CategoryId.Value)).ReturnsAsync(SeedData.GetCategorySample());
        _mockSubcategoryService.Setup(x => x.GetAllSubcategories(mockData.SubcategoriesIds)).Returns(mockSubcategories.ToAsyncEnumerable());

        // Act
        var result = await _controller.Put(1, mockData);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }

    [TestMethod]
    public async Task PutProduct_ReturnsNoContent()
    {
        // Arrange
        var mockData = new ProductUpdateDTO { Id = 1, Name = "Product1", Price = 0, CategoryId = 1, SubcategoriesIds = new List<int> { 1, 2 } };
        var mockSubcategories = new List<Subcategory> { new Subcategory(), new Subcategory() };
        _mockProductService.Setup(x => x.GetProductById(1)).ReturnsAsync(SeedData.GetProductSample());
        _mockCategoryService.Setup(x => x.GetCategoryById(mockData.CategoryId.Value)).ReturnsAsync(SeedData.GetCategorySample());
        _mockSubcategoryService.Setup(x => x.GetAllSubcategories(mockData.SubcategoriesIds)).Returns(mockSubcategories.ToAsyncEnumerable());

        // Act
        var result = await _controller.Put(1, mockData);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task PostProduct_NullCategory_ReturnsNotFound()
    {
        // Arrange
        var mockData = new ProductCreateDTO { Name = "Product1", Price = 0, CategoryId = 1, SubcategoriesIds = new List<int> { 1, 2 } };
        _mockCategoryService.Setup(x => x.GetCategoryById(mockData.CategoryId)).ReturnsAsync((Category)null!);

        // Act
        var result = await _controller.Post(mockData);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));

    }

    [TestMethod]
    public async Task PostProduct_NullSubcategory_ReturnsNotFound()
    {
        // Arrange
        var mockData = new ProductCreateDTO { Name = "Product1", Price = 0, CategoryId = 1, SubcategoriesIds = new List<int> { 1, 2 } };
        var mockSubcategories = new List<Subcategory?> { new Subcategory(), null };
        _mockCategoryService.Setup(x => x.GetCategoryById(mockData.CategoryId)).ReturnsAsync(SeedData.GetCategorySample());
        _mockSubcategoryService.Setup(x => x.GetAllSubcategories(mockData.SubcategoriesIds)).Returns(mockSubcategories.ToAsyncEnumerable());

        // Act
        var result = await _controller.Post(mockData);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
    }

    [TestMethod]
    public async Task PostProduct_ReturnsProductReadDTO()
    {
        // Arrange
        var mockData = new ProductCreateDTO { Name = "Product1", Price = 0, CategoryId = 1, SubcategoriesIds = new List<int> { 1, 2 } };
        var mockSubcategories = new List<Subcategory> { new Subcategory(), new Subcategory() };
        _mockCategoryService.Setup(x => x.GetCategoryById(mockData.CategoryId)).ReturnsAsync(SeedData.GetCategorySample());
        _mockSubcategoryService.Setup(x => x.GetAllSubcategories(mockData.SubcategoriesIds)).Returns(mockSubcategories.ToAsyncEnumerable());
        _mockProductService.Setup(x => x.CreateProduct(It.IsAny<Product>()));
        _mockProductService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync(SeedData.GetProductSample());

        // Act
        var result = await _controller.Post(mockData);
        var product = result.GetObjectResult();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(product, typeof(ProductReadDTO));
    }

    [TestMethod]
    public async Task DeleteProduct_ReturnsNotFound()
    {
        // Arrange
        _mockProductService.Setup(x => x.GetProductById(1)).ReturnsAsync((Product)null!);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task DeleteProduct_ReturnsNoContent()
    {
        // Arrange
        _mockProductService.Setup(x => x.GetProductById(1)).ReturnsAsync(SeedData.GetProductSample());
        _mockProductService.Setup(x => x.DeleteProduct(1));

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

}
