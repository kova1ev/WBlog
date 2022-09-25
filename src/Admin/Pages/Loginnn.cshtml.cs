using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WBlog.Admin.Models;

namespace WBlog.Admin.Pages;

public class Loginnn : PageModel
{
    //[BindProperty] public LoginModel LoginModel { get; set; }

    public void OnGet()
    {
    }

    public void OnPost(string email, string password)
    {
        Console.WriteLine($"{email} - {password}");
       // Console.WriteLine($"{LoginModel.Email} - {LoginModel.Password}");
    }
}