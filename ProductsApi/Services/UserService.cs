using ProductsApi.DAL.Repository;
using ProductsApi.DTOs;
using ProductsApi.Interfaces;
using ProductsApi.Models;

namespace ProductsApi.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;

    public UserService(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<User?> Authenticate(string email, string password)
    {
        return await _repository.GetAsync(x => x.Email == email && x.Password == password);
    }

    public async Task<User> CreateUser(User user)
    {
        await _repository.AddAsync(user);
        return user;
    }

    public async Task<User> DeleteUser(int id)
    {
        var user = await CheckIfUserExists(id);
        await _repository.DeleteAsync(user);
        return user;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _repository.GetAsync(x => x.Email == email);
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<User> UpdateUser(User user)
    {
        await _repository.UpdateAsync(user);
        return user;
    }

    private async Task<User> CheckIfUserExists(int id)
    {
        return await _repository.GetByIdAsync(id) ?? throw new Exception("User not found");
    }
}