using ProductsApi.Interfaces;

namespace ProductsApi.Services;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    public bool VerifyHashedPassword(string password, string hashedPassword) =>
        BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}
