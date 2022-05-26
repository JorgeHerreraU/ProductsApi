using ProductsApi.DTOs;
using ProductsApi.Models;

namespace ProductsApi.Interfaces;
public interface IUserService
{
    Task<User?> Authenticate(string email, string password);
    Task<IEnumerable<User>> GetUsers();
    Task<User?> GetUserById(int id);
    Task<User?> GetUserByEmail(string email);
    Task<User> CreateUser(User user);
    Task<User> UpdateUser(User user);
    Task<User> DeleteUser(int id);
}