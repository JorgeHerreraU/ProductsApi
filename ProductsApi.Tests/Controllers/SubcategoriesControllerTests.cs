using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductsApi.Controllers;
using ProductsApi.Interfaces;
using ProductsApi.Profiles;
using ProductsApi.DTOs;
using System.Linq;
using ProductsApi.Models;
using Microsoft.AspNetCore.Mvc;
using ProductsApi.Enums;
using System.Collections.Generic;

namespace ProductsApi.Tests.Controllers;

[TestClass]
public class SubcategoriesControllerTests
{
    private readonly SubcategoriesController _controller;
    private readonly Mock<ICategoryService> _mockCategoryService;
    private readonly Mock<ISubcategoryService> _mockSubcategoryService;
    private readonly IMapper _mapper;

    public SubcategoriesControllerTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<SubcategoriesProfile>();
        });
        _mapper = config.CreateMapper();
        _mockCategoryService = new Mock<ICategoryService>();
        _mockSubcategoryService = new Mock<ISubcategoryService>();
        _controller = new SubcategoriesController(_mockCategoryService.Object, _mockSubcategoryService.Object, _mapper);
    }

    [TestMethod]
    public async Task GetSubcategories_ReturnsAllSubcategories()
    {
        // Arrange
        _mockSubcategoryService.Setup(x => x.GetAllSubcategories()).ReturnsAsync(SeedData.GetSubcategoriesSample());

        // Act
        var result = await _controller.Get();
        var subcategories = result.GetObjectResult();

        // Assert
        Assert.IsNotNull(subcategories);
        Assert.AreEqual(5, subcategories.Count());
    }

    [TestMethod]
    public async Task GetSubcategory_ReturnsSubcategoryReadDTO()
    {
        // Arrange
        _mockSubcategoryService.Setup(x => x.GetSubcategoryById(1)).ReturnsAsync(SeedData.GetSubcategorySample());

        // Act
        var result = await _controller.Get(1);
        var subcategory = result.GetObjectResult();

        // Assert
        Assert.IsNotNull(subcategory);
        Assert.AreEqual(1, subcategory.Id);
        Assert.IsInstanceOfType(subcategory, typeof(SubcategoryReadDTO));
    }

    [TestMethod]
    public async Task GetSubcategory_ReturnsNotFound()
    {
        // Arrange
        _mockSubcategoryService.Setup(x => x.GetSubcategoryById(1)).ReturnsAsync((Subcategory)null!);

        // Act
        var result = await _controller.Get(1);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task PostSubcategory_ReturnsSubcategoryReadDTO()
    {
        // Arrange
        var mockData = new SubcategoryCreateDTO { Name = "Subcategory 1", Description = "Description 1", Priority = Priority.Low };

        _mockSubcategoryService.Setup(x => x.CreateSubcategory(_mapper.Map<Subcategory>(mockData)));

        // Act
        var result = await _controller.Post(mockData);
        var subcategory = result.GetObjectResult();

        // Assert
        Assert.IsNotNull(subcategory);
        Assert.IsInstanceOfType(subcategory, typeof(SubcategoryReadDTO));
        Assert.AreEqual("Subcategory 1", subcategory.Name);
        Assert.AreEqual("Description 1", subcategory.Description);
        Assert.AreEqual("low", subcategory.Priority);
    }

    [TestMethod]
    public async Task PutSubcategory_ReturnsNoContent()
    {
        // Arrange
        var mockRequest = new SubcategoryUpdateDTO { Id = 1, Name = "Subcategory 1", Description = "Description 1" };
        _mockSubcategoryService.Setup(x => x.GetSubcategoryById(1)).ReturnsAsync(SeedData.GetSubcategorySample());

        // Act
        var result = await _controller.Put(1, mockRequest);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task PutSubcategory_NullSubcategory_ReturnsNotFound()
    {
        // Arrange
        var mockRequest = new SubcategoryUpdateDTO { Id = 1, Name = "Subcategory 1", Description = "Description 1" };
        _mockSubcategoryService.Setup(x => x.GetSubcategoryById(1)).ReturnsAsync((Subcategory)null!);

        // Act
        var result = await _controller.Put(1, mockRequest);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task PutSubcategory_NullCategory_ReturnsNotFound()
    {
        // Arrange
        var mockData = new SubcategoryUpdateDTO { Id = 1, Name = "Subcategory 1", Description = "Description 1", CategoriesIds = new int[] { 1, 2, 3 } };
        var mockCategories = new List<Category?> { new Category(), null };

        _mockSubcategoryService.Setup(x => x.GetSubcategoryById(1)).ReturnsAsync((Subcategory)null!);
        _mockCategoryService.Setup(x => x.GetAllCategories(mockData.CategoriesIds)).Returns(mockCategories.ToAsyncEnumerable());

        // Act
        var result = await _controller.Put(1, mockData);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task DeleteSubcategory_ReturnsNoContent()
    {
        // Arrange
        _mockSubcategoryService.Setup(x => x.GetSubcategoryById(1)).ReturnsAsync(SeedData.GetSubcategorySample());

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task DeleteSubcategory_ReturnsNotFound()
    {
        // Arrange
        _mockSubcategoryService.Setup(x => x.GetSubcategoryById(1)).ReturnsAsync((Subcategory)null!);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

}