namespace WBlog.Domain.Entity
{
    public class Admin : BaseEntity
    {
        public string? Email { get; init; }
        public string? Password { get; init; }
        public string Role { get; init; } = "admin";
    }
}