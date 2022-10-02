using Microsoft.AspNetCore.Identity;

namespace WBlog.Infrastructure.Data;

public class User
{
    public string? Email { get; init; }
    public string? Password { get; init; }
    public string? Role { get; init; }

}

public class UserService
{
    private readonly List<User> _users;
    public UserService()
    {
        _users = new()
    {
        new User {Email = "admin@mail.com",Password="12345",Role="admin"}
    };

    }


    public User? GetUser(string userEmail)
    {
        return _users.FirstOrDefault(u => u.Email == userEmail);
    }
}