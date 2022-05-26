using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsApi.DTOs;
using ProductsApi.Interfaces;
using ProductsApi.Models;

namespace ProductsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public UsersController(IUserService userService, IMapper mapper, IPasswordService passwordService)
        {
            _userService = userService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> Get()
        {
            var users = await _userService.GetUsers();

            return Ok(_mapper.Map<List<UserReadDTO>>(users));
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDTO>> Get(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null) return NotFound();

            return Ok(_mapper.Map<UserReadDTO>(user));
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UserUpdateDTO userIn)
        {
            if (id != userIn.Id) return BadRequest();

            var user = await _userService.GetUserById(id);

            if (user == null) return NotFound();

            _mapper.Map(userIn, user);

            try
            {
                await _userService.UpdateUser(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserReadDTO>> Post(UserCreateDTO userIn)
        {
            var user = _mapper.Map<User>(userIn);

            if (await _userService.GetUserByEmail(user.Email) != null)
            {
                return BadRequest("User with this email already exists");
            }

            user.Password = _passwordService.HashPassword(userIn.Password);

            await _userService.CreateUser(user);

            return CreatedAtAction("Get", new { id = user.Id }, _mapper.Map<UserReadDTO>(user));
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null) return NotFound();

            await _userService.DeleteUser(id);

            return NoContent();
        }

        private bool UserExists(int id) => _userService.GetUserById(id) != null;
    }
}
