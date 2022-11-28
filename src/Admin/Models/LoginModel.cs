using System.ComponentModel.DataAnnotations;
using WBlog.Core.Domain;

namespace WBlog.Admin.Models;

public class LoginModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}