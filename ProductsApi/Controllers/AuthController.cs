using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsApi.DTOs;
using ProductsApi.Interfaces;
using ProductsApi.Models;

namespace ProductsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordService;
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IMapper mapper, IPasswordService passwordService, IAuthService authService, IUserService userService)
    {
        _mapper = mapper;
        _passwordService = passwordService;
        _authService = authService;
        _userService = userService;
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<ActionResult<UserReadDTO>> Login(UserLoginDTO userIn)
    {
        var user = await _userService.GetUserByEmail(userIn.Email);

        if (user == null) return BadRequest("User not found");

        var isPasswordValid = _passwordService.VerifyHashedPassword(userIn.Password, user.Password);

        if (!isPasswordValid) return Unauthorized();

        return Ok(await UpdateUserToken(user));
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(UserRegisterDTO userIn)
    {
        var userExists = await CheckIfUserExistsByEmail(userIn.Email);

        if (userExists == true) return BadRequest("User already exists");

        var user = _mapper.Map<User>(userIn);

        user.Password = _passwordService.HashPassword(userIn.Password);

        await _userService.CreateUser(user);

        return Ok();
    }

    [HttpPost]
    [Route("Refresh")]
    [AllowAnonymous]
    public async Task<ActionResult<UserReadDTO>> RefreshToken(RefreshTokenDTO refreshTokenDTO)
    {
        if (string.IsNullOrEmpty(refreshTokenDTO.Token) || string.IsNullOrEmpty(refreshTokenDTO.RefreshToken)) return Unauthorized();

        var email = _authService.GetEmailFromToken(refreshTokenDTO.Token);

        if (string.IsNullOrEmpty(email)) return Unauthorized();

        var user = await _userService.GetUserByEmail(email);

        if (user == null) return Unauthorized();

        if (user.RefreshToken != refreshTokenDTO.RefreshToken || user.RefreshTokenExpiration <= DateTime.Now) return Unauthorized();

        return Ok(await UpdateUserToken(user));

    }

    private async Task<UserReadDTO> UpdateUserToken(User user)
    {
        var userReadDTO = _mapper.Map<UserReadDTO>(user);

        var token = _authService.GenerateToken(user);

        var refreshToken = _authService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiration = DateTime.Now.AddDays(7);
        userReadDTO.Token = token;
        userReadDTO.RefreshToken = refreshToken;

        await _userService.UpdateUser(user);

        return userReadDTO;
    }
    private async Task<bool> CheckIfUserExistsByEmail(string email) => await _userService.GetUserByEmail(email) != null;
}
