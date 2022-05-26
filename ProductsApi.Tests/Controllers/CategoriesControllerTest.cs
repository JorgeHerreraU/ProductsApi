using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductsApi.Controllers;
using ProductsApi.DTOs;
using ProductsApi.Models;
using ProductsApi.Profiles;
using ProductsApi.Interfaces;
using System.Linq;
using System;

namespace ProductsApi.Tests.Controllers;
[TestClass]
public class CategoriesControllerTest
{
    private readonly CategoriesController _controller;
    private readonly Mock<ICategoryService> _mockCategoryService;
    private readonly Mock<ISubcategoryService> _mockSubcategoryService;
    private readonly IMapper _mapper;

    public CategoriesControllerTest()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CategoriesProfile>();
        });
        _mapper = config.CreateMapper();
        _mockCategoryService = new Mock<ICategoryService>();
        _mockSubcategoryService = new Mock<ISubcategoryService>();
        _controller = new CategoriesController(_mockCategoryService.Object, _mockSubcategoryService.Object, _mapper);
    }

    [TestMethod]
    public async Task GetCategories_ReturnsAllCategories()
    {
        // Arrange
        _mockCategoryService.Setup(x => x.GetAllCategories()).ReturnsAsync(SeedData.GetCategoriesSample());

        // Act
        var result = await _controller.Get();
        var categories = result.GetObjectResult();

        // Assert
        Assert.AreEqual(5, categories.Count());
    }

    [TestMethod]
    public async Task GetCategory_ReturnsCategoryReadDTO()
    {
        // Arrange
        _mockCategoryService.Setup(x => x.GetCategoryById(1)).ReturnsAsync(SeedData.GetCategorySample());

        // Act
        var result = await _controller.Get(1);
        var category = result.GetObjectResult();

        // Assert
        Assert.AreEqual(1, category.Id);
        Assert.IsInstanceOfType(category, typeof(CategoryReadDTO));
    }

    [TestMethod]
    public async Task GetCategory_ReturnsNotFound()
    {
        // Arrange
        _mockCategoryService.Setup(x => x.GetCategoryById(1)).ReturnsAsync((Category)null!);

        // Act
        var result = await _controller.Get(1);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task PostCategory_ReturnsCategoryReadDTO()
    {
        // Arrange
        var mockData = new CategoryCreateDTO { Name = "Category1", Description = "Description" };

        _mockCategoryService.Setup(x => x.CreateCategory(_mapper.Map<Category>(mockData)));

        // Act
        var result = await _controller.Post(mockData);
        var category = result.GetObjectResult();

        // Assert
        Assert.IsInstanceOfType(category, typeof(CategoryReadDTO));
        Assert.AreEqual(mockData.Name, category.Name);
        Assert.AreEqual(mockData.Description, category.Description);
    }

    [TestMethod]
    public async Task PutCategory_ReturnsBadRequest()
    {
        // Arrange
        var mockData = new CategoryUpdateDTO { Id = 2, Name = "Category1", Description = "Description" };

        // Act
        var result = await _controller.Put(1, mockData);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
    }

    [TestMethod]
    public async Task PutCategory_ReturnsNotFound()
    {
        // Arrange
        var mockData = new CategoryUpdateDTO { Id = 1, Name = "Category1", Description = "Description" };

        _mockCategoryService.Setup(x => x.GetCategoryById(1)).ReturnsAsync((Category)null!);

        // Act
        var result = await _controller.Put(1, mockData);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task PutCategory_NullSubcategory_ReturnsNotFound()
    {
        // Arrange
        var mockData = new CategoryUpdateDTO { Id = 1, Name = "Category1", Description = "Description", SubcategoriesIds = new int[] { 1, 2, 3 } };
        var mockSubcategories = new List<Subcategory?> { new Subcategory(), null };

        _mockCategoryService.Setup(x => x.GetCategoryById(1)).ReturnsAsync(_mapper.Map<Category>(mockData));
        _mockSubcategoryService.Setup(x => x.GetAllSubcategories(mockData.SubcategoriesIds)).Returns(mockSubcategories.ToAsyncEnumerable());

        // Act
        var result = await _controller.Put(1, mockData);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }

    [TestMethod]
    public async Task PutCategory_ReturnsNoContent()
    {
        // Arrange
        var mockData = new CategoryUpdateDTO { Id = 1, Name = "Category1", Description = "Description" };
        _mockCategoryService.Setup(x => x.GetCategoryById(1)).ReturnsAsync(_mapper.Map<Category>(mockData));

        // Act
        var result = await _controller.Put(1, mockData);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task DeleteCategory_ReturnsNoContent()
    {
        // Arrange
        _mockCategoryService.Setup(x => x.GetCategoryById(1)).ReturnsAsync(SeedData.GetCategorySample());

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task DeleteCategory_ReturnsNotFound()
    {
        // Arrange
        _mockCategoryService.Setup(x => x.GetCategoryById(1)).ReturnsAsync((Category)null!);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }


}