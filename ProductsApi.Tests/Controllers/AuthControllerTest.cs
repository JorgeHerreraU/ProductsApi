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
public class AuthControllerTest
{
    private readonly AuthController _controller;
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IPasswordService> _mockPasswordService;
    private readonly Mock<IAuthService> _mockJWTAuthService;
    private readonly IMapper _mapper;

    public AuthControllerTest()
    {
        _mockUserService = new Mock<IUserService>();
        _mockJWTAuthService = new Mock<IAuthService>();
        _mockPasswordService = new Mock<IPasswordService>();
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UsersProfile>();
        });
        _mapper = config.CreateMapper();
        _controller = new AuthController(_mapper, _mockPasswordService.Object, _mockJWTAuthService.Object, _mockUserService.Object);
    }

    [TestMethod]
    public async Task Login_ReturnsUserReadDTO()
    {
        // Arrange
        var mockData = new UserLoginDTO() { Email = "user@user.com", Password = "123" };
        _mockUserService.Setup(x => x.GetUserByEmail(mockData.Email)).ReturnsAsync(SeedData.GetUserSample());
        _mockPasswordService.Setup(x => x.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
        _mockJWTAuthService.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns("123456encryptedtoken");

        // Act
        var result = await _controller.Login(mockData);
        var user = result.GetObjectResult();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(user, typeof(UserReadDTO));
        Assert.AreEqual(user.Token, "123456encryptedtoken");
    }

    [TestMethod]
    public async Task Login_ReturnsBadRequest()
    {
        // Arrange
        var mockData = new UserLoginDTO() { Email = "test@test.com", Password = "123" };
        _mockUserService.Setup(x => x.GetUserByEmail(mockData.Email)).ReturnsAsync((User)null!);

        // Act
        var result = await _controller.Login(mockData);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task Login_ReturnsUnauthorized()
    {
        // Arrange
        var mockData = new UserLoginDTO() { Email = "test@test.com", Password = "123" };
        _mockUserService.Setup(x => x.GetUserByEmail(mockData.Email)).ReturnsAsync(SeedData.GetUserSample());
        _mockPasswordService.Setup(x => x.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

        // Act
        var result = await _controller.Login(mockData);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.Result, typeof(UnauthorizedResult));
    }

    [TestMethod]
    public async Task Register_ReturnsOk()
    {
        // Arrange
        var mockData = new UserRegisterDTO() { Email = "test@test.com", Password = "123", FirstName = "Test", LastName = "Test" };
        _mockUserService.Setup(x => x.GetUserByEmail(mockData.Email)).ReturnsAsync((User)null!);
        _mockPasswordService.Setup(x => x.HashPassword(mockData.Password));
        _mockUserService.Setup(x => x.CreateUser(It.IsAny<User>()));


        // Act
        var result = await _controller.Register(mockData);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public async Task Register_ReturnsBadRequest()
    {
        // Arrange
        var mockData = new UserRegisterDTO() { Email = "test@test.com", Password = "123", FirstName = "Test", LastName = "Test" };
        _mockUserService.Setup(x => x.GetUserByEmail(mockData.Email)).ReturnsAsync(SeedData.GetUserSample());

        // Act
        var result = await _controller.Register(mockData);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }
}
