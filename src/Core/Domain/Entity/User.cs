namespace WBlog.Core.Domain.Entity;

public class User : BaseEntity
{
    public string? Email { get; init; }
    public string? Password { get; init; }
    public string Role { get; init; } = "admin";
}