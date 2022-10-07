using System.ComponentModel.DataAnnotations;
using WBlog.Core.Domain;

namespace WBlog.Admin.Models;

public class LoginModel
{
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }



    public Login ToLogin()
    {
        return new Login { Email = Email, Password = Password };
    }
}