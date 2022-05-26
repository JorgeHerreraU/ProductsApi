using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductsApi.Controllers;
using ProductsApi.DTOs;
using ProductsApi.Enums;
using ProductsApi.Interfaces;
using ProductsApi.Models;
using ProductsApi.Profiles;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsApi.Tests.Controllers;

[TestClass]
public class UsersControllerTest
{
    private readonly UsersController _controller;
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IPasswordService> _mockPasswordService;
    private readonly IMapper _mapper;

    public UsersControllerTest()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UsersProfile>();
        });
        _mapper = config.CreateMapper();
        _mockUserService = new Mock<IUserService>();
        _mockPasswordService = new Mock<IPasswordService>();

        _controller = new UsersController(_mockUserService.Object, _mapper, _mockPasswordService.Object);
    }

    [TestMethod]
    public async Task GetAllUsers_ReturnsAllUsers()
    {
        // Arrange
        _mockUserService.Setup(x => x.GetUsers()).ReturnsAsync(SeedData.GetUsersSample());

        // Act
        var result = await _controller.Get();
        var users = result.GetObjectResult();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(SeedData.GetUsersSample().Count, users.Count());
    }

    [TestMethod]
    public async Task GetUser_ReturnsUser()
    {
        // Arrange
        _mockUserService.Setup(x => x.GetUserById(1)).ReturnsAsync(SeedData.GetUserSample());

        // Act
        var result = await _controller.Get(1);
        var user = result.GetObjectResult();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, user.Id);
    }

    [TestMethod]
    public async Task GetUser_ReturnsNotFound()
    {
        // Arrange
        _mockUserService.Setup(x => x.GetUserById(1)).ReturnsAsync((User)null!);

        // Act
        var result = await _controller.Get(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    }
    [TestMethod]
    public async Task PutUser_WrongId_ReturnsBadRequest()
    {
        // Arrange
        var mockData = new UserUpdateDTO
        {
            Id = 1,
            Firstname = "Test",
            Lastname = "Test",
            Email = "test@test.com"
        };

        // Act
        var result = await _controller.Put(2, mockData);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
    }

    [TestMethod]
    public async Task PutUser_ReturnsNotFound()
    {
        // Arrange
        var mockData = new UserUpdateDTO
        {
            Id = 1,
            Firstname = "Test",
            Lastname = "Test",
            Email = "test@test.com"
        };
        _mockUserService.Setup(x => x.GetUserById(1)).ReturnsAsync((User)null!);

        // Act
        var result = await _controller.Put(1, mockData);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task PutUser_ReturnsNoContent()
    {
        // Arrange
        var mockData = new UserUpdateDTO
        {
            Id = 1,
            Firstname = "Test",
            Lastname = "Test",
            Email = "test@test.com"
        };
        var user = SeedData.GetUserSample();
        _mockUserService.Setup(x => x.GetUserById(1)).ReturnsAsync(user);

        // Act
        var result = await _controller.Put(1, mockData);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
        Assert.AreEqual(mockData.Firstname, user.Firstname);
        Assert.AreEqual(mockData.Lastname, user.Lastname);
        Assert.AreEqual(mockData.Email, user.Email);
    }

    [TestMethod]
    public async Task PostUser_ReturnsUserReadDTO()
    {
        // Arrange
        var mockData = new UserCreateDTO
        {
            Firstname = "Test",
            Lastname = "Test",
            Email = "test@test.com",
            Password = "test"
        };
        _mockUserService.Setup(x => x.CreateUser(It.IsAny<User>()));

        // Act
        var result = await _controller.Post(mockData);
        var user = result.GetObjectResult();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(user, typeof(UserReadDTO));
    }

    [TestMethod]
    public async Task DeleteUser_ReturnsNotFound()
    {
        // Arrange
        _mockUserService.Setup(x => x.GetUserById(1)).ReturnsAsync((User)null!);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task DeleteUser_ReturnsNoContent()
    {
        // Arrange
        var user = SeedData.GetUserSample();
        _mockUserService.Setup(x => x.GetUserById(1)).ReturnsAsync(user);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

}
